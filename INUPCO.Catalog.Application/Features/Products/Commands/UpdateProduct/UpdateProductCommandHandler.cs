using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Domain.Entities.Products;
using INUPCO.Catalog.Domain.Exceptions;
using MediatR;

namespace INUPCO.Catalog.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException($"Product with ID {request.Id} was not found.");

        product.UpdateDetails(request.Name, request.TradeCode);
        await _productRepository.UpdateAsync(product);

        return product;
    }
} 