using Ambev.DeveloperEvaluation.Application.Sales.Common;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Handler for processing <see cref="GetAllSalesQuery"/> requests.
/// </summary>
public class GetAllSalesHandler : IRequestHandler<GetAllSalesQuery, IEnumerable<SaleResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSalesHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllSalesHandler"/> class.
    /// </summary>
    public GetAllSalesHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<GetAllSalesHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<SaleResult>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllSalesQuery");

        var sales = await _saleRepository.GetAllAsync(cancellationToken);

        _logger.LogInformation("{Count} sales retrieved", sales.Count());

        return _mapper.Map<IEnumerable<SaleResult>>(sales);
    }
}