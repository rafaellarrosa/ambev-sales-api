using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing <see cref="CreateSaleCommand"/> requests.
/// Responsible for applying business rules and persisting the sale.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ILogger<CreateSaleHandler> _logger;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="bus">The queue provider</param>
    /// <param name="logger">The logger provider</param>
    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IBus bus, ILogger<CreateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _bus = bus;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting CreateSaleHandler with Request: {Request}", request);

        var validator = new CreateSaleCommandValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);

        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var sale = _mapper.Map<Sale>(request);

        foreach (var item in sale.Items)
        {
            item.ApplyDiscount();
        }

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        await _bus.Publish(new SaleCreatedEvent(createdSale.Id, createdSale.Customer, createdSale.SaleDate));

        var result = _mapper.Map<CreateSaleResult>(createdSale);

        _logger.LogInformation("Sale created successfully with Id {SaleId}", sale.Id);

        return result;
    }
}