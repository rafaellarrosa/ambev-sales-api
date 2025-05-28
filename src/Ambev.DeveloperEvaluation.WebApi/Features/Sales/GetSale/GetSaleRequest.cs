namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Request model for retrieving a sale by its ID.
/// </summary>
public class GetSaleRequest
{
    /// <summary>
    /// The sale identifier.
    /// </summary>
    public Guid Id { get; set; }
}