using System.Linq.Expressions;
using AlfaBetBackendExercise.Contracts;
using AlfaBetBackendExercise.Database.Context;
using AlfaBetBackendExercise.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlfaBetBackendExercise.Logic.Events;

public class EventsHandler
{
    private readonly AlfaBetDbContext _dbContext;

    public EventsHandler(AlfaBetDbContext dbContext)
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

    public async Task<IEnumerable<Event>> RetrieveEventsAsync(RetrieveEventsRequest request,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Event> retrieveEventsQuery = _dbContext.Events.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.LocationFilter))
        {
            retrieveEventsQuery = retrieveEventsQuery.Where(e => EF.Functions.ILike(e.Location, $"%{request.LocationFilter}%"));
        }

        Expression<Func<Event, object>> eventColumnSelector = EventSortByColumnSelector(request.SortBy);

        retrieveEventsQuery = request.SortOrder switch
        {
            RetrieveEventsSortOrder.Asc or RetrieveEventsSortOrder.Ascending => retrieveEventsQuery.OrderBy(eventColumnSelector),
            RetrieveEventsSortOrder.Desc or RetrieveEventsSortOrder.Descending => retrieveEventsQuery.OrderByDescending(eventColumnSelector),
            _ => throw new ArgumentOutOfRangeException(nameof(request.SortOrder), "This really shouldn't have been possible")
        };

        return await retrieveEventsQuery.ToListAsync(cancellationToken);
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

    private static Expression<Func<Event, object>> EventSortByColumnSelector(RetrieveEventsSortBy? sortBy) => sortBy switch
    {
        RetrieveEventsSortBy.Date => e => e.Date,
        RetrieveEventsSortBy.Popularity => e => e.ParticipantsAmount ?? int.MinValue,
        RetrieveEventsSortBy.CreationDate => e => e.CreationDate,
        _ => e => e.Id
    };
}