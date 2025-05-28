using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Interface for accessing and managing sales in the data store.
/// Defines methods for CRUD operations on sales.
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Creates a new sale in the database.
    /// </summary>
    /// <param name="sale">The sale to create.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created sale.</returns>
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the sale.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The sale if found, null otherwise.</returns>
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all sales in the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of all sales.</returns>
    Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels a sale by its unique identifier.
    /// </summary>
    /// <param name="id">The sale identifier to cancel.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the sale was cancelled, false if not found.</returns>
    Task<bool> CancelAsync(Guid id, CancellationToken cancellationToken = default);
}