namespace Messaging.Events.Base;

/// <summary>
/// Base class for all integration events used for communication between microservices.
/// Implements the Event pattern for asynchronous message-based communication.
/// </summary>
public abstract class IntegrationEvent
{
    /// <summary>
    /// Gets the unique identifier for this event instance
    /// </summary>
    public Guid Id { get; }
    
    /// <summary>
    /// Gets the UTC timestamp when this event occurred
    /// </summary>
    public DateTime OccurredOn { get; }
    
    /// <summary>
    /// Gets the type name of the event (used for routing and deserialization)
    /// </summary>
    public string EventType { get; }

    /// <summary>
    /// Initializes a new integration event with a new ID and current timestamp
    /// </summary>
    protected IntegrationEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        EventType = GetType().Name;
    }

    /// <summary>
    /// Initializes a new integration event with specified ID and timestamp.
    /// Used for event reconstruction or testing.
    /// </summary>
    /// <param name="id">The unique identifier for the event</param>
    /// <param name="occurredOn">The timestamp when the event occurred</param>
    protected IntegrationEvent(Guid id, DateTime occurredOn)
    {
        Id = id;
        OccurredOn = occurredOn;
        EventType = GetType().Name;
    }
}
