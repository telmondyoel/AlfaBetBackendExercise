namespace AlfaBetBackendExercise.Contracts;

public record ScheduleEventContract(
    string Summary,
    string Location,
    DateTimeOffset Date,
    int? Participants);