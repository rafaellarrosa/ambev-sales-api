using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="CancelSaleHandler"/> class.
/// </summary>
public class CancelSaleHandlerTests
{
    private readonly ISaleRepository _repository;
    private readonly CancelSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelSaleHandlerTests"/> class.
    /// </summary>
    public CancelSaleHandlerTests()
    {
        _repository = Substitute.For<ISaleRepository>();
        _handler = new CancelSaleHandler(_repository);
    }

    /// <summary>
    /// Tests that a valid cancellation is processed successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale ID When cancelling Then returns success")]
    public async Task Handle_ValidRequest_CancelsSuccessfully()
    {
        // Given
        var saleId = Guid.NewGuid();
        _repository.CancelAsync(saleId, Arg.Any<CancellationToken>()).Returns(true);

        // When
        var act = async () => await _handler.Handle(new CancelSaleCommand(saleId), CancellationToken.None);

        // Then
        await act.Should().NotThrowAsync();
        await _repository.Received(1).CancelAsync(saleId, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that cancellation throws an exception if the sale does not exist.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale ID When cancelling Then throws exception")]
    public async Task Handle_InvalidRequest_ThrowsException()
    {
        // Given
        var saleId = Guid.NewGuid();
        _repository.CancelAsync(saleId, Arg.Any<CancellationToken>()).Returns(false);

        // When
        var act = async () => await _handler.Handle(new CancelSaleCommand(saleId), CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Sale with Id {saleId} not found.");
    }
}
