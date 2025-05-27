using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleCommand"/>.
/// Defines rules for validating customer name, branch, and sale items.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of <see cref="CreateSaleCommandValidator"/> with defined validation rules.
    /// </summary>
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.Customer).NotEmpty().WithMessage("Customer is required.");
        RuleFor(x => x.Branch).NotEmpty().WithMessage("Branch is required.");

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId).NotEmpty().WithMessage("ProductId is required.");
            item.RuleFor(i => i.ProductName).NotEmpty().WithMessage("ProductName is required.");
            item.RuleFor(i => i.Quantity)
                .InclusiveBetween(1, 20)
                .WithMessage("Quantity must be between 1 and 20.");
            item.RuleFor(i => i.UnitPrice)
                .GreaterThan(0)
                .WithMessage("UnitPrice must be greater than zero.");
        });
    }
}