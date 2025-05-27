using Ambev.DeveloperEvaluation.Application.Sales.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Query to retrieve all sales in the system.
/// </summary>
public class GetAllSalesQuery : IRequest<IEnumerable<SaleResult>>
{
}