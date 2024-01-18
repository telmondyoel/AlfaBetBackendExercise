namespace AlfaBetBackendExercise.Contracts;

public record UpdateEventContract(
    string Summary,
    string Location,
    DateTimeOffset Date,
    int? Participants);