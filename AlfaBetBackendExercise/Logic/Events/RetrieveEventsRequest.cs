namespace AlfaBetBackendExercise.Logic.Events;

public record RetrieveEventsRequest
{
    public string? LocationFilter { get; init; }
};