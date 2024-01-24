using System.Linq.Expressions;
using AlfaBetBackendExercise.Contracts;
using AlfaBetBackendExercise.Database.Context;
using AlfaBetBackendExercise.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlfaBetBackendExercise.Logic.Events;

public class EventsHandler
{
    private readonly AlfaBetDbContext _dbContext;
    private readonly TimeProvider _timeProvider;

    public EventsHandler(AlfaBetDbContext dbContext, TimeProvider timeProvider)
    {
        _dbContext = dbContext;
        _timeProvider = timeProvider;
    }

    public async Task<EventViewResponse> ScheduleEventAsync(ScheduleEventRequest scheduleEventRequest,
        CancellationToken cancellationToken = default)
    {
        Event eventToCreate = new()
        {
            Summary = scheduleEventRequest.Summary,
            Location = scheduleEventRequest.Location,
            Date = scheduleEventRequest.Date.UtcDateTime,
            ParticipantsAmount = scheduleEventRequest.Participants,
            CreationDate = _timeProvider.GetUtcNow().UtcDateTime
        };

        await _dbContext.Events.AddAsync(eventToCreate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return EventViewResponse.FromEventEntity(eventToCreate);
    }

    public async Task<IEnumerable<EventViewResponse>> RetrieveEventsAsync(RetrieveEventsRequest request,
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

        List<Event> retrievedEvents = await retrieveEventsQuery.ToListAsync(cancellationToken);

        return retrievedEvents.Select(EventViewResponse.FromEventEntity);
    }

    public async Task<EventViewResponse?> RetrieveEventAsync(int eventId, CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == eventId, cancellationToken) is { } retrievedEvent)
        {
            return EventViewResponse.FromEventEntity(retrievedEvent);
        }

        return null;
    }

    public async Task<EventViewResponse?> UpdateEventAsync(int eventId, UpdateEventRequest updateEventRequest,
        CancellationToken cancellationToken = default)
    {
        Event? eventToUpdate = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == eventId, cancellationToken);

        if (eventToUpdate is null)
        {
            return null;
        }

        eventToUpdate.Summary = updateEventRequest.Summary;
        eventToUpdate.Location = updateEventRequest.Location;
        eventToUpdate.Date = updateEventRequest.Date.UtcDateTime;
        eventToUpdate.ParticipantsAmount = updateEventRequest.Participants;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return EventViewResponse.FromEventEntity(eventToUpdate);
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