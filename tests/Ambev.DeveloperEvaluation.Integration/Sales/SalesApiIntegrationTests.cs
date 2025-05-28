using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.Integration.Common;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Sales;

/// <summary>
/// Contains integration tests for the Sales API.
/// Tests cover end-to-end scenarios including creation, retrieval, listing, and cancellation.
/// </summary>
public class SalesApiIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of <see cref="SalesApiIntegrationTests"/>.
    /// </summary>
    /// <param name="factory">The WebApplicationFactory instance for the API.</param>
    public SalesApiIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Tests that creating a sale returns 201 Created with the sale ID.
    /// </summary>
    [Fact(DisplayName = "POST /api/sales should create sale and return 201")]
    public async Task Post_CreateSale_ShouldReturnCreated()
    {
        // Arrange
        var request = new CreateSaleRequest
        {
            Customer = "Company ABC",
            Branch = "Branch 1",
            Items = new List<CreateSaleRequest.SaleItemRequest>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Product X",
                    Quantity = 5,
                    UnitPrice = 100
                }
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/sales", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
        content.Should().NotBeNull();
        content!.Success.Should().BeTrue();
        content.Data.Id.Should().NotBeEmpty();
    }

    /// <summary>
    /// Tests that retrieving all sales returns 200 OK with the list.
    /// </summary>
    [Fact(DisplayName = "GET /api/sales should return list of sales")]
    public async Task Get_ListSales_ShouldReturnOk()
    {
        // Act
        var response = await _client.GetAsync("/api/sales");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    /// <summary>
    /// Tests that retrieving a sale by ID returns 200 OK if it exists.
    /// </summary>
    [Fact(DisplayName = "GET /api/sales/{id} should return sale if exists")]
    public async Task Get_SaleById_ShouldReturnOk()
    {
        // First create a sale
        var createRequest = new CreateSaleRequest
        {
            Customer = "Company XYZ",
            Branch = "Branch 2",
            Items = new List<CreateSaleRequest.SaleItemRequest>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Product Y",
                    Quantity = 4,
                    UnitPrice = 50
                }
            }
        };

        var createResponse = await _client.PostAsJsonAsync("/api/sales", createRequest);
        var createdContent = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();

        // Act
        var response = await _client.GetAsync($"/api/sales/{createdContent!.Data.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    /// <summary>
    /// Tests that cancelling a sale returns 200 OK.
    /// </summary>
    [Fact(DisplayName = "DELETE /api/sales/{id} should cancel sale")]
    public async Task Delete_CancelSale_ShouldReturnOk()
    {
        // Arrange — cria uma sale primeiro
        var createRequest = new CreateSaleRequest
        {
            Customer = "Company DEF",
            Branch = "Branch 3",
            Items = new List<CreateSaleRequest.SaleItemRequest>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Product Z",
                    Quantity = 7,
                    UnitPrice = 30
                }
            }
        };

        var createResponse = await _client.PostAsJsonAsync("/api/sales", createRequest);
        var createdContent = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();

        // Act — faz o DELETE
        var response = await _client.DeleteAsync($"/api/sales/{createdContent!.Data.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}