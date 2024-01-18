namespace AlfaBetBackendExercise.Contracts;

public record ScheduleEventRequest(
    string Summary,
    string Location,
    DateTimeOffset Date,
    int? Participants);