using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="GetAllSalesHandler"/> class.
/// </summary>
public class GetAllSalesHandlerTests
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSalesHandler> _logger;
    private readonly GetAllSalesHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllSalesHandlerTests"/> class.
    /// Sets up the repository, mapper, and logger mocks.
    /// </summary>
    public GetAllSalesHandlerTests()
    {
        _repository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetAllSalesHandler>>();

        _handler = new GetAllSalesHandler(_repository, _mapper, _logger);
    }

    /// <summary>
    /// Tests that when sales exist, the handler returns the expected sales.
    /// </summary>
    [Fact(DisplayName = "Given sales exist When retrieving all Then returns sales")]
    public async Task Handle_ReturnsAllSales()
    {
        // Given
        var sales = new List<Sale> { new Sale { Id = Guid.NewGuid() } };
        var salesResult = new List<SaleResult> { new SaleResult { Id = sales[0].Id } };

        _repository.GetAllAsync(Arg.Any<CancellationToken>()).Returns(sales);
        _mapper.Map<IEnumerable<SaleResult>>(sales).Returns(salesResult);

        // When
        var result = await _handler.Handle(new GetAllSalesQuery(), CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result.First().Id.Should().Be(sales[0].Id);
    }
}