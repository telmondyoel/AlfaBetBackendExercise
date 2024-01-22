using AlfaBetBackendExercise.Database.Context;
using AlfaBetBackendExercise.Database.Entities;
using AlfaBetBackendExercise.Logic.Events.Reminders.Sinks;
using Microsoft.EntityFrameworkCore;

namespace AlfaBetBackendExercise.Logic.Events.Reminders;

public class EventsRemindersBackgroundService : BackgroundService
{
    private static readonly TimeSpan EventsReminderTimeBeforeEvent = TimeSpan.FromMinutes(30);
    private static readonly TimeSpan EventsRemindersRefreshPeriod = TimeSpan.FromMinutes(1);

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TimeProvider _timeProvider;
    private readonly IEnumerable<IEventReminderSink> _eventReminderSinks;

    public EventsRemindersBackgroundService(IServiceScopeFactory scopeFactory, TimeProvider timeProvider,
        IEnumerable<IEventReminderSink> eventReminderSinks)
    {
        _scopeFactory = scopeFactory;
        _timeProvider = timeProvider;
        _eventReminderSinks = eventReminderSinks;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer periodicTimer = new(EventsRemindersRefreshPeriod, _timeProvider);

        while (!stoppingToken.IsCancellationRequested && await periodicTimer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                List<Event> eventsForSendingReminders = await RetrieveEventsForSendingRemindersAsync(stoppingToken);

                await TriggerRemindersAsync(eventsForSendingReminders, stoppingToken);
                await UpdateEventsRemindersTriggeredInDatabaseAsync(eventsForSendingReminders, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    private async Task<List<Event>> RetrieveEventsForSendingRemindersAsync(CancellationToken stoppingToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AlfaBetDbContext>();
        DateTime now = _timeProvider.GetUtcNow().UtcDateTime;

        return await dbContext.Events
            .Where(e => !e.ReminderTriggered && e.Date <= now.Add(EventsReminderTimeBeforeEvent))
            .ToListAsync(cancellationToken: stoppingToken);
    }

    private async Task TriggerRemindersAsync(IEnumerable<Event> events, CancellationToken stoppingToken)
    {
        IEnumerable<Task> eventSinksTasks = events.SelectMany(@event => _eventReminderSinks.Select(async sink =>
        {
            if (await sink.ShouldTriggerReminderAsync(@event, stoppingToken))
            {
                await sink.TriggerReminderAsync(@event, stoppingToken);
            }
        }));

        await Task.WhenAll(eventSinksTasks);
    }

    private async Task UpdateEventsRemindersTriggeredInDatabaseAsync(List<Event> events, CancellationToken stoppingToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AlfaBetDbContext>();

        foreach (Event eventToUpdate in events)
        {
            eventToUpdate.ReminderTriggered = true;
            dbContext.Update(eventToUpdate);
        }

        await dbContext.SaveChangesAsync(stoppingToken);
    }
}