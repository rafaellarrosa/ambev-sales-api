using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for <see cref="CreateSaleRequestValidator"/>.
/// Tests cover API request validation for customer, branch, and items.
/// </summary>
public class CreateSaleRequestValidatorTests
{
    private readonly CreateSaleRequestValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleRequestValidatorTests"/> class.
    /// </summary>
    public CreateSaleRequestValidatorTests()
    {
        _validator = new CreateSaleRequestValidator();
    }

    /// <summary>
    /// Tests that a valid CreateSaleRequest passes validation.
    /// </summary>
    [Fact(DisplayName = "Given valid CreateSaleRequest When validating Then should pass")]
    public void Given_ValidRequest_When_Validating_Then_ShouldPass()
    {
        // Arrange
        var request = new CreateSaleRequest
        {
            Customer = "Company ABC",
            Branch = "Branch X",
            Items = new List<CreateSaleRequest.SaleItemRequest>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Product X",
                    Quantity = 5,
                    UnitPrice = 100
                }
            }
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that an empty branch fails validation.
    /// </summary>
    [Fact(DisplayName = "Given empty branch When validating Then should fail")]
    public void Given_EmptyBranch_When_Validating_Then_ShouldFail()
    {
        // Arrange
        var request = new CreateSaleRequest
        {
            Customer = "Company ABC",
            Branch = "",
            Items = new List<CreateSaleRequest.SaleItemRequest>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Product X",
                    Quantity = 5,
                    UnitPrice = 100
                }
            }
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Branch);
    }

    /// <summary>
    /// Tests that an item with quantity below 1 fails validation.
    /// </summary>
    [Fact(DisplayName = "Given item quantity less than 1 When validating Then should fail")]
    public void Given_ItemQuantityLessThan1_When_Validating_Then_ShouldFail()
    {
        // Arrange
        var request = new CreateSaleRequest
        {
            Customer = "Company ABC",
            Branch = "Branch X",
            Items = new List<CreateSaleRequest.SaleItemRequest>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Product X",
                    Quantity = 0,
                    UnitPrice = 100
                }
            }
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor("Items[0].Quantity");
    }

    /// <summary>
    /// Tests that an item with empty product name fails validation.
    /// </summary>
    [Fact(DisplayName = "Given item with empty product name When validating Then should fail")]
    public void Given_ItemWithEmptyProductName_When_Validating_Then_ShouldFail()
    {
        // Arrange
        var request = new CreateSaleRequest
        {
            Customer = "Company ABC",
            Branch = "Branch X",
            Items = new List<CreateSaleRequest.SaleItemRequest>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "",
                    Quantity = 5,
                    UnitPrice = 100
                }
            }
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor("Items[0].ProductName");
    }
}
