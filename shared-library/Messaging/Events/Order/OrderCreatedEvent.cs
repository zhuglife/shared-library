using Messaging.Events.Base;

namespace Messaging.Events.Order;

/// <summary>
/// Event published when a new order is successfully created in the system.
/// Other services can subscribe to this event to trigger their workflows (e.g., payment, inventory, notification).
/// </summary>
public class OrderCreatedEvent : IntegrationEvent
{
    /// <summary>
    /// Gets or sets the unique identifier of the created order
    /// </summary>
    public Guid OrderId { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the customer who placed the order
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Gets or sets the total amount of the order including all items
    /// </summary>
    public decimal TotalAmount { get; set; }
    
    /// <summary>
    /// Gets or sets the current status of the order (e.g., "Pending", "Confirmed")
    /// </summary>
    public string Status { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the timestamp when the order was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the list of items included in the order
    /// </summary>
    public List<OrderItemDto> Items { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of OrderCreatedEvent with default values
    /// </summary>
    public OrderCreatedEvent() { }

    /// <summary>
    /// Initializes a new instance of OrderCreatedEvent with specified values
    /// </summary>
    /// <param name="orderId">The unique identifier of the order</param>
    /// <param name="customerId">The unique identifier of the customer</param>
    /// <param name="totalAmount">The total amount of the order</param>
    /// <param name="status">The initial status of the order</param>
    /// <param name="items">The list of items in the order</param>
    public OrderCreatedEvent(
        Guid orderId, 
        Guid customerId, 
        decimal totalAmount, 
        string status,
        List<OrderItemDto> items)
    {
        OrderId = orderId;
        CustomerId = customerId;
        TotalAmount = totalAmount;
        Status = status;
        Items = items ?? new List<OrderItemDto>();
        CreatedAt = DateTime.UtcNow;
    }
}
