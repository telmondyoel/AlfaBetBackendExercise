using AlfaBetBackendExercise.Contracts;
using AlfaBetBackendExercise.Database.Entities;
using AlfaBetBackendExercise.Logic.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlfaBetBackendExercise.Controllers;

[Authorize]
[Route("events")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly EventsHandler _eventsHandler;

    public EventsController(EventsHandler eventsHandler)
    {
        _eventsHandler = eventsHandler;
    }

    [HttpPost]
    public async Task<IActionResult> ScheduleEvent([FromBody] ScheduleEventContract scheduleEvent,
        CancellationToken cancellationToken = default)
    {
        Event createdEvent = await _eventsHandler.ScheduleEventAsync(scheduleEvent, cancellationToken);
        return CreatedAtAction(nameof(ScheduleEvent), createdEvent);
    }

    [HttpGet]
    public async Task<IEnumerable<Event>> RetrieveEvents(string? locationFilter,
        RetrieveEventsSortBy? sortBy,
        RetrieveEventsSortOrder sortOrder = RetrieveEventsSortOrder.Ascending,
        CancellationToken cancellationToken = default)
    {
        RetrieveEventsRequest request = new()
        {
            LocationFilter = locationFilter,
            SortBy = sortBy,
            SortOrder = sortOrder
        };

        return await _eventsHandler.RetrieveEventsAsync(request, cancellationToken);
    }

    [HttpGet("{eventId:int}")]
    public async Task<IActionResult> RetrieveEvent(int eventId, CancellationToken cancellationToken = default)
    {
        if (await _eventsHandler.RetrieveEventAsync(eventId, cancellationToken) is { } retrievedEvent)
        {
            return Ok(retrievedEvent);
        }

        return NotFound($"Event {eventId} does not exist");
    }

    [HttpPut("{eventId:int}")]
    public async Task<IActionResult> UpdateEvent(int eventId,
        [FromBody] UpdateEventContract updateEvent,
        CancellationToken cancellationToken = default)
    {
        if (await _eventsHandler.UpdateEventAsync(eventId, updateEvent, cancellationToken) is { } updatedEvent)
        {
            return Ok(updatedEvent);
        }

        return NotFound($"Event {eventId} does not exist");
    }

    [HttpDelete("{eventId:int}")]
    public async Task<IActionResult> DeleteEvent(int eventId, CancellationToken cancellationToken = default)
    {
        if (await _eventsHandler.DeleteEventAsync(eventId, cancellationToken))
        {
            return Ok($"Event {eventId} was deleted successfully");
        }

        return NotFound($"Event {eventId} does not exist");
    }
}