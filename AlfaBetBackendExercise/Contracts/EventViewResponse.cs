using AlfaBetBackendExercise.Database.Entities;

namespace AlfaBetBackendExercise.Contracts;

public record EventViewResponse
{
    public int Id { get; init; }
    public string Summary { get; init; }
    public string Location { get; init; }
    public DateTime Date { get; init; }
    public int? Participants { get; init; }
    public DateTime CreationDate { get; init; }

    public static EventViewResponse FromEventEntity(Event @event)
    {
        return new EventViewResponse
        {
            Id = @event.Id,
            Summary = @event.Summary,
            Location = @event.Location,
            Date = @event.Date,
            Participants = @event.ParticipantsAmount,
            CreationDate = @event.CreationDate
        };
    }
}