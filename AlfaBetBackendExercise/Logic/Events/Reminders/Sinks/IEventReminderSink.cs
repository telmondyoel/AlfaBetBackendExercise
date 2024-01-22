using AlfaBetBackendExercise.Database.Entities;

namespace AlfaBetBackendExercise.Logic.Events.Reminders.Sinks;

public interface IEventReminderSink
{
    ValueTask<bool> ShouldTriggerReminderAsync(Event @event, CancellationToken cancellationToken);
    Task TriggerReminderAsync(Event @event, CancellationToken cancellationToken);
}