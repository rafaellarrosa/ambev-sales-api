using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing sales operations.
/// Provides endpoints to create, retrieve, list, and cancel sales.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<SalesController> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="SalesController"/>.
    /// </summary>
    /// <param name="mediator">The Mediator instance for handling commands and queries.</param>
    /// <param name="mapper">The AutoMapper instance for mapping between layers.</param>
    /// <param name="logger">The logger instance.</param>
    public SalesController(IMediator mediator, IMapper mapper, ILogger<SalesController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new sale.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received CreateSale request: {@Request}", request);

        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("CreateSale request validation failed: {@Errors}", validationResult.Errors);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<CreateSaleCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Sale created successfully with Id {SaleId}", result.Id);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<CreateSaleResponse>(result)
        });
    }

    /// <summary>
    /// Retrieves the details of a sale by its unique identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received GetSale request for SaleId: {SaleId}", id);

        var request = new GetSaleRequest { Id = id };
        var validator = new GetSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("GetSale request validation failed for SaleId: {SaleId}", id);
            return BadRequest(validationResult.Errors);
        }

        var query = _mapper.Map<GetSaleByIdQuery>(request);
        var response = await _mediator.Send(query, cancellationToken);

        if (response == null)
        {
            _logger.LogWarning("Sale with Id {SaleId} not found", id);
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"Sale with Id {id} not found"
            });
        }

        _logger.LogInformation("Sale with Id {SaleId} retrieved successfully", id);

        return Ok(new ApiResponseWithData<GetSaleResponse>
        {
            Success = true,
            Message = "Sale retrieved successfully",
            Data = _mapper.Map<GetSaleResponse>(response)
        });
    }

    /// <summary>
    /// Lists all sales.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<ListSalesResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListSales(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received ListSales request");

        var response = await _mediator.Send(new GetAllSalesQuery(), cancellationToken);

        _logger.LogInformation("Returning {Count} sales", response.Count());

        return Ok(new ApiResponseWithData<IEnumerable<ListSalesResponse>>
        {
            Success = true,
            Message = "Sales listed successfully",
            Data = _mapper.Map<IEnumerable<ListSalesResponse>>(response)
        });
    }

    /// <summary>
    /// Cancels a sale by its identifier.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received CancelSale request for SaleId: {SaleId}", id);

        var request = new CancelSaleRequest { Id = id };
        var validator = new CancelSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("CancelSale request validation failed for SaleId: {SaleId}", id);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<CancelSaleCommand>(request);
        await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Sale with Id {SaleId} cancelled successfully", id);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale cancelled successfully"
        });
    }
}