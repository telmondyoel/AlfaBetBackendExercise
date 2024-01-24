namespace AlfaBetBackendExercise.Logic.Events;

public record RetrieveEventsRequest
{
    public string? LocationFilter { get; init; }
    public RetrieveEventsSortBy? SortBy { get; init; }
    public RetrieveEventsSortOrder SortOrder { get; init; }
};