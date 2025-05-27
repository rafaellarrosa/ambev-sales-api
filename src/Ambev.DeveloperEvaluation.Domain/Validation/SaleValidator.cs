using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the <see cref="Sale"/> entity.
/// Ensures that customer, branch, and items are valid.
/// </summary>
public class SaleValidator : AbstractValidator<Sale>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaleValidator"/> class.
    /// </summary>
    public SaleValidator()
    {
        RuleFor(sale => sale.Customer)
            .NotEmpty()
            .WithMessage("Customer is required.");

        RuleFor(sale => sale.Branch)
            .NotEmpty()
            .WithMessage("Branch is required.");

        RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("Sale must have at least one item.");
    }
}