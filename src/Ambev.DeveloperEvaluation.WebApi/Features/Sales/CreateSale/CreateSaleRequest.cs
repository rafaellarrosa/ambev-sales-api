namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Request model for creating a new sale.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// The customer name.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// The branch where the sale occurs.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// The list of items included in the sale.
    /// </summary>
    public List<SaleItemRequest> Items { get; set; } = new();

    /// <summary>
    /// Represents an item in the sale.
    /// </summary>
    public class SaleItemRequest
    {
        /// <summary>
        /// The unique identifier of the product.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// The product name.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// The quantity of the product.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}