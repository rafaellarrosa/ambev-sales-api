using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new sale transaction.
/// </summary>
/// <remarks>
/// Contains customer details, branch information, and a list of sale items.
/// Triggers the creation of a sale with applicable business rules for discounts and limits.
/// </remarks>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    /// <summary>
    /// Gets or sets the customer name associated with the sale.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch where the sale is being registered.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of items included in the sale.
    /// </summary>
    public List<SaleItemDto> Items { get; set; } = new();

    /// <summary>
    /// Represents a sale item within the CreateSaleCommand.
    /// </summary>
    public class SaleItemDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// Must comply with the sale business rules.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}