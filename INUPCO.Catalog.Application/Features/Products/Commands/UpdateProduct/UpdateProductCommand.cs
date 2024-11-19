using INUPCO.Catalog.Domain.Entities.Products;
using MediatR;

namespace INUPCO.Catalog.Application.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<Product>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string TradeCode { get; init; } = string.Empty;
} 