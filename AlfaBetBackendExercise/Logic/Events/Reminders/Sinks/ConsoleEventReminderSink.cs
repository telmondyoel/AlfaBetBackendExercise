using AlfaBetBackendExercise.Database.Entities;

namespace AlfaBetBackendExercise.Logic.Events.Reminders.Sinks;

public class ConsoleEventReminderSink : IEventReminderSink
{
    public ValueTask<bool> ShouldTriggerReminderAsync(Event @event, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(true);
    }

    public Task TriggerReminderAsync(Event @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[Console 1] Triggered reminder on event {@event.Id}");
        return Task.CompletedTask;
    }
}