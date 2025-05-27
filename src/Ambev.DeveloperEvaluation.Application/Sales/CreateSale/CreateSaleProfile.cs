using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping between <see cref="CreateSaleCommand"/>, <see cref="Sale"/>, and <see cref="CreateSaleResult"/>.
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of <see cref="CreateSaleProfile"/> and configures mappings.
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>();
        CreateMap<CreateSaleCommand.SaleItemDto, SaleItem>();
        CreateMap<Sale, CreateSaleResult>();
    }
}