namespace Stock;

/// <summary>
/// Representa uma transação de venda de estoque.
/// </summary>
public class SaleStockDto
{
    /// <summary>
    /// Identificador único da venda.
    /// </summary>
    public Guid SaleId
    {
        get; set;
    }

    /// <summary>
    /// Identificador do produto vendido.
    /// </summary>
    public Guid ProductId
    {
        get; set;
    }

    /// <summary>
    /// Nome do produto.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Quantidade vendida.
    /// </summary>
    public int Quantity
    {
        get; set;
    }

    /// <summary>
    /// Preço unitário de venda.
    /// </summary>
    public decimal UnitPrice
    {
        get; set;
    }

    /// <summary>
    /// Valor total da venda (Quantity * UnitPrice).
    /// </summary>
    public decimal TotalAmount
    {
        get; set;
    }

    /// <summary>
    /// Data e hora da venda.
    /// </summary>
    public DateTime SaleDate
    {
        get; set;
    }

    /// <summary>
    /// Identificador do cliente (opcional).
    /// </summary>
    public Guid? CustomerId
    {
        get; set;
    }

    /// <summary>
    /// Nome do cliente (opcional).
    /// </summary>
    public string? CustomerName
    {
        get; set;
    }

    /// <summary>
    /// Observações sobre a venda.
    /// </summary>
    public string? Notes
    {
        get; set;
    }
}
