using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale transaction in the system.
/// This entity includes the sale number, customer, branch, date, status, and sale items.
/// </summary>
/// <remarks>
/// Applies business rules for cancellation and tracks total amount based on items.
/// </remarks>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets or sets the sale number.
    /// Used for human-readable tracking of the sale.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the customer name associated with this sale.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch where the sale was processed.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; private set; }

    /// <summary>
    /// Gets the total amount for the sale, calculated from all sale items.
    /// </summary>
    public decimal TotalAmount => Items.Sum(i => i.TotalAmount);

    /// <summary>
    /// Gets or sets the collection of items associated with this sale.
    /// </summary>
    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Sale"/> class.
    /// </summary>
    public Sale()
    {
        SaleDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the sale entity using the <see cref="SaleValidator"/> rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Customer name not empty</list>
    /// <list type="bullet">Branch name not empty</list>
    /// <list type="bullet">At least one sale item present</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }

    /// <summary>
    /// Cancels the sale if it is not already cancelled.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the sale has already been cancelled.
    /// </exception>
    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Sale is already cancelled.");

        IsCancelled = true;
    }
}