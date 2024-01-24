namespace AlfaBetBackendExercise.Contracts;

public record UpdateEventRequest(
    string Summary,
    string Location,
    DateTimeOffset Date,
    int? Participants);