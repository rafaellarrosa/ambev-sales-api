using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="GetSaleByIdHandler"/> class.
/// </summary>
public class GetSaleByIdHandlerTests
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSaleByIdHandler> _logger;
    private readonly GetSaleByIdHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleByIdHandlerTests"/> class.
    /// Sets up repository, mapper, and logger mocks.
    /// </summary>
    public GetSaleByIdHandlerTests()
    {
        _repository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetSaleByIdHandler>>();

        _handler = new GetSaleByIdHandler(_repository, _mapper, _logger);
    }

    /// <summary>
    /// Tests that when a valid sale ID is provided, the handler returns the correct sale.
    /// </summary>
    [Fact(DisplayName = "Given valid sale ID When retrieving Then returns sale")]
    public async Task Handle_ValidRequest_ReturnsSale()
    {
        // Given
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId };
        var saleResult = new SaleResult { Id = saleId };

        _repository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);
        _mapper.Map<SaleResult>(sale).Returns(saleResult);

        // When
        var result = await _handler.Handle(new GetSaleByIdQuery(saleId), CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(saleId);
    }

    /// <summary>
    /// Tests that when an invalid sale ID is provided, the handler throws an exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale ID When retrieving Then throws exception")]
    public async Task Handle_InvalidRequest_ThrowsException()
    {
        // Given
        var saleId = Guid.NewGuid();
        _repository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns((Sale?)null);

        // When
        var act = async () => await _handler.Handle(new GetSaleByIdQuery(saleId), CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Sale with Id {saleId} not found.");
    }
}