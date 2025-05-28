using Ambev.DeveloperEvaluation.Application.Sales.Common;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

/// <summary>
/// Handler for processing <see cref="GetSaleByIdQuery"/> requests.
/// </summary>
public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, SaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleByIdHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public GetSaleByIdHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<SaleResult> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken)
                   ?? throw new InvalidOperationException($"Sale with Id {request.SaleId} not found.");

        return _mapper.Map<SaleResult>(sale);
    }
}