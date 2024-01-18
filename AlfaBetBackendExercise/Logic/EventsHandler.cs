using AlfaBetBackendExercise.Contracts;
using AlfaBetBackendExercise.Database.Context;
using AlfaBetBackendExercise.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlfaBetBackendExercise.Logic;

public class EventsHandler
{
    private readonly AlfaBetContext _dbContext;

    public EventsHandler(AlfaBetContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Event> ScheduleEventAsync(ScheduleEventContract scheduleEvent,
        CancellationToken cancellationToken = default)
    {
        Event eventToCreate = new()
        {
            Summary = scheduleEvent.Summary,
            Location = scheduleEvent.Location,
            Date = scheduleEvent.Date.UtcDateTime,
            ParticipantsAmount = scheduleEvent.Participants,
            CreationDate = DateTimeOffset.Now.UtcDateTime
        };

        await _dbContext.Events.AddAsync(eventToCreate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return eventToCreate;
    }

    public async Task<IEnumerable<Event>> RetrieveAllEventsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Events.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Event?> RetrieveEventAsync(int eventId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == eventId, cancellationToken);
    }

    public async Task<Event?> UpdateEventAsync(int eventId, UpdateEventContract updateEvent,
        CancellationToken cancellationToken = default)
    {
        Event? eventToUpdate = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == eventId, cancellationToken);

        if (eventToUpdate is null)
        {
            return eventToUpdate;
        }

        eventToUpdate.Summary = updateEvent.Summary;
        eventToUpdate.Location = updateEvent.Location;
        eventToUpdate.Date = updateEvent.Date.UtcDateTime;
        eventToUpdate.ParticipantsAmount = updateEvent.Participants;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return eventToUpdate;
    }

    public async Task<bool> DeleteEventAsync(int eventId, CancellationToken cancellationToken = default)
    {
        int deletedEvents = await _dbContext.Events.Where(e => e.Id == eventId).ExecuteDeleteAsync(cancellationToken);
        return deletedEvents > 0;
    }
}