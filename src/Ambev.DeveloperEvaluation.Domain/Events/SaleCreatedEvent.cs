using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Event that indicates a sale has been created.
/// </summary>
/// <param name="SaleId">The unique identifier of the sale.</param>
/// <param name="Customer">The customer name.</param>
/// <param name="SaleDate">The date the sale was made.</param>
public record SaleCreatedEvent(Guid SaleId, string Customer, DateTime SaleDate) : IDomainEvent;