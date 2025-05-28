using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

/// <summary>
/// Provides methods for generating valid and invalid test data for CreateSaleHandler.
/// </summary>
public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleCommand> SaleFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.Customer, f => f.Company.CompanyName())
        .RuleFor(s => s.Branch, f => f.Address.City())
        .RuleFor(s => s.Items, f => new List<CreateSaleCommand.SaleItemDto>
        {
            new()
            {
                ProductId = Guid.NewGuid(),
                ProductName = f.Commerce.ProductName(),
                Quantity = f.Random.Int(4, 10),
                UnitPrice = f.Random.Decimal(10, 100)
            }
        });

    /// <summary>
    /// Generates a valid CreateSaleCommand.
    /// </summary>
    public static CreateSaleCommand GenerateValidCommand() => SaleFaker.Generate();
}