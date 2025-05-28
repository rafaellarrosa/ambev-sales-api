using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleRequest"/>.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of <see cref="CreateSaleRequestValidator"/>.
    /// </summary>
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.Customer).NotEmpty();
        RuleFor(x => x.Branch).NotEmpty();

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId).NotEmpty();
            item.RuleFor(i => i.ProductName).NotEmpty();
            item.RuleFor(i => i.Quantity).InclusiveBetween(1, 20);
            item.RuleFor(i => i.UnitPrice).GreaterThan(0);
        });
    }
}