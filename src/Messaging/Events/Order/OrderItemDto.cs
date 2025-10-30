namespace Messaging.Events.Order;

/// <summary>
/// Data Transfer Object representing a single item in an order.
/// Used to transfer order line item information between services.
/// </summary>
public class OrderItemDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the product
    /// </summary>
    public Guid ProductId { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the product at the time of order
    /// </summary>
    public string ProductName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the quantity of this product ordered
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Gets or sets the price per unit at the time of order
    /// </summary>
    public decimal UnitPrice { get; set; }
    
    /// <summary>
    /// Gets the total price for this line item (Quantity × UnitPrice)
    /// </summary>
    public decimal TotalPrice => Quantity * UnitPrice;
}
