using Ambev.DeveloperEvaluation.Application.Sales.Common;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

/// <summary>
/// Handler for processing <see cref="GetSaleByIdQuery"/> requests.
/// </summary>
public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, SaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSaleByIdHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleByIdHandler"/> class.
    /// </summary>
    public GetSaleByIdHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<GetSaleByIdHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<SaleResult> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetSaleByIdQuery for SaleId: {SaleId}", request.SaleId);

        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);

        if (sale == null)
        {
            _logger.LogWarning("Sale with Id {SaleId} not found", request.SaleId);
            throw new InvalidOperationException($"Sale with Id {request.SaleId} not found.");
        }

        _logger.LogInformation("Sale with Id {SaleId} retrieved successfully", request.SaleId);

        return _mapper.Map<SaleResult>(sale);
    }
}