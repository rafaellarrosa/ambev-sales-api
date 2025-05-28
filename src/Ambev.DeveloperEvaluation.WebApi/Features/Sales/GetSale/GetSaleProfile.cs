using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Profile for mapping between API and Application GetSale models.
/// </summary>
public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<GetSaleRequest, GetSaleByIdQuery>();
        CreateMap<SaleResult, GetSaleResponse>();
        CreateMap<SaleItemResult, SaleItemResponse>();
    }
}