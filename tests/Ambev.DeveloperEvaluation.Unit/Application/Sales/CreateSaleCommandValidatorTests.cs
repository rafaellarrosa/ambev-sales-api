using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for <see cref="CreateSaleCommandValidator"/>.
/// Tests cover validation of customer, branch, and item details.
/// </summary>
public class CreateSaleCommandValidatorTests
{
    private readonly CreateSaleCommandValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleCommandValidatorTests"/> class.
    /// </summary>
    public CreateSaleCommandValidatorTests()
    {
        _validator = new CreateSaleCommandValidator();
    }

    /// <summary>
    /// Tests that a valid CreateSaleCommand passes validation.
    /// </summary>
    [Fact(DisplayName = "Given valid CreateSaleCommand When validating Then should pass")]
    public void Given_ValidCommand_When_Validating_Then_ShouldPass()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that an empty customer fails validation.
    /// </summary>
    [Fact(DisplayName = "Given empty customer When validating Then should fail")]
    public void Given_EmptyCustomer_When_Validating_Then_ShouldFail()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        command.Customer = string.Empty;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Customer);
    }

    /// <summary>
    /// Tests that an item with quantity over 20 fails validation.
    /// </summary>
    [Fact(DisplayName = "Given item quantity over 20 When validating Then should fail")]
    public void Given_ItemQuantityOver20_When_Validating_Then_ShouldFail()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        command.Items[0].Quantity = 25;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor("Items[0].Quantity");
    }

    /// <summary>
    /// Tests that an item with unit price less than or equal to zero fails validation.
    /// </summary>
    [Fact(DisplayName = "Given item with unit price <= 0 When validating Then should fail")]
    public void Given_ItemWithUnitPriceZero_When_Validating_Then_ShouldFail()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        command.Items[0].UnitPrice = 0;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor("Items[0].UnitPrice");
    }
}
