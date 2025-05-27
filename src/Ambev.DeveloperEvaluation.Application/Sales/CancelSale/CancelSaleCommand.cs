using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Command for cancelling an existing sale.
/// </summary>
/// <remarks>
/// Cancelling a sale sets its status to cancelled and prevents further modifications.
/// </remarks>
public class CancelSaleCommand : IRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale to cancel.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelSaleCommand"/> class.
    /// </summary>
    /// <param name="saleId">The unique identifier of the sale.</param>
    public CancelSaleCommand(Guid saleId)
    {
        SaleId = saleId;
    }
}