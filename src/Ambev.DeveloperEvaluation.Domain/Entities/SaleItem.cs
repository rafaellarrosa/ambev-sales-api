using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item in a sale transaction.
/// Includes product details, quantity, pricing, and applicable discounts.
/// </summary>
/// <remarks>
/// Handles the logic for discount application based on quantity.
/// </remarks>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the product's unique identifier.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of the product in this sale.
    /// Must be between 1 and 20, inclusive.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the price per unit of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets the discount applied based on the quantity rules.
    /// </summary>
    public decimal Discount { get; private set; }

    /// <summary>
    /// Gets the total amount for this item after applying the discount.
    /// </summary>
    public decimal TotalAmount => (UnitPrice * Quantity) - Discount;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleItem"/> class.
    /// </summary>
    public SaleItem() { }

    /// <summary>
    /// Applies the discount rules based on the quantity.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the quantity exceeds the maximum allowed (20).
    /// </exception>
    public void ApplyDiscount()
    {
        if (Quantity < 4)
        {
            Discount = 0;
        }
        else if (Quantity >= 4 && Quantity < 10)
        {
            Discount = UnitPrice * Quantity * 0.10m;
        }
        else if (Quantity >= 10 && Quantity <= 20)
        {
            Discount = UnitPrice * Quantity * 0.20m;
        }
        else
        {
            throw new InvalidOperationException("Cannot sell more than 20 identical items.");
        }
    }
}
