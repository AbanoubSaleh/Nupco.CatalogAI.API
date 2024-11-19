using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Domain.Entities.Products;
using INUPCO.Catalog.Domain.Exceptions;
using MediatR;

namespace INUPCO.Catalog.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly IManufacturerRepository _manufacturerRepository;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IManufacturerRepository manufacturerRepository)
    {
        _productRepository = productRepository;
        _manufacturerRepository = manufacturerRepository;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = await _manufacturerRepository.GetByIdAsync(request.ManufacturerId);
        
        if (manufacturer == null)
        {
            throw new NotFoundException($"Manufacturer with ID {request.ManufacturerId} was not found.");
        }

        var product = Product.Create(request.Name, request.TradeCode, manufacturer);
        return await _productRepository.AddAsync(product);
    }
} 