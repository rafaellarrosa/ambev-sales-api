using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handler for processing <see cref="CancelSaleCommand"/> requests.
/// Responsible for cancelling a sale.
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ILogger<CancelSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="logger">The logger instance.</param>
    public CancelSaleHandler(ISaleRepository saleRepository, ILogger<CancelSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting CancelSaleHandler for SaleId: {SaleId}", request.SaleId);

        var success = await _saleRepository.CancelAsync(request.SaleId, cancellationToken);

        if (!success)
        {
            _logger.LogWarning("Sale with Id {SaleId} not found for cancellation", request.SaleId);
            throw new InvalidOperationException($"Sale with Id {request.SaleId} not found.");
        }

        _logger.LogInformation("Sale with Id {SaleId} cancelled successfully", request.SaleId);
    }
}