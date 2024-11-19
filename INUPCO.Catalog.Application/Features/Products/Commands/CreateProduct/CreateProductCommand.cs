using INUPCO.Catalog.Domain.Entities.Products;
using MediatR;

namespace INUPCO.Catalog.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<Product>
{
    public string Name { get; init; } = string.Empty;
    public int ManufacturerId { get; init; }
    public string TradeCode { get; init; } = string.Empty;
    public int SubsidiaryId { get; init; }
} 