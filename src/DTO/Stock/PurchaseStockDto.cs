namespace Stock;
/// <summary>
/// Representa uma transação de compra de estoque.
/// </summary>
public class PurchaseStockDto
{
    /// <summary>
    /// Identificador único da compra.
    /// </summary>
    public Guid PurchaseId
    {
        get; set;
    }

    /// <summary>
    /// Identificador do produto comprado.
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
    /// Quantidade comprada.
    /// </summary>
    public int Quantity
    {
        get; set;
    }

    /// <summary>
    /// Preço unitário de compra.
    /// </summary>
    public decimal UnitCost
    {
        get; set;
    }

    /// <summary>
    /// Valor total da compra (Quantity * UnitCost).
    /// </summary>
    public decimal TotalCost
    {
        get; set;
    }

    /// <summary>
    /// Data e hora da compra.
    /// </summary>
    public DateTime PurchaseDate
    {
        get; set;
    }

    /// <summary>
    /// Identificador do fornecedor.
    /// </summary>
    public Guid SupplierId
    {
        get; set;
    }

    /// <summary>
    /// Nome do fornecedor.
    /// </summary>
    public string SupplierName { get; set; } = string.Empty;

    /// <summary>
    /// Número da nota fiscal.
    /// </summary>
    public string? InvoiceNumber
    {
        get; set;
    }

    /// <summary>
    /// Observações sobre a compra.
    /// </summary>
    public string? Notes
    {
        get; set;
    }
}
