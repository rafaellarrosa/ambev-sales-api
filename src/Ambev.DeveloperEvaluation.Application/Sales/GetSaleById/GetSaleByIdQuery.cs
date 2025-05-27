using Ambev.DeveloperEvaluation.Application.Sales.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

/// <summary>
/// Query to retrieve a sale by its unique identifier.
/// </summary>
public class GetSaleByIdQuery : IRequest<SaleResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleByIdQuery"/> class.
    /// </summary>
    /// <param name="saleId">The sale identifier.</param>
    public GetSaleByIdQuery(Guid saleId)
    {
        SaleId = saleId;
    }
}