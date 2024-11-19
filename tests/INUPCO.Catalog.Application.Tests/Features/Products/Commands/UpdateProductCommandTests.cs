using Xunit;
using Moq;
using FluentAssertions;
using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Domain.Entities.Products;
using INUPCO.Catalog.Application.Tests.Common;
using INUPCO.Catalog.Application.Features.Products.Commands.UpdateProduct;

namespace INUPCO.Catalog.Application.Tests.Features.Products.Commands
{
    public class UpdateProductCommandTests
    {
        private readonly Mock<IProductRepository> _mockProductRepo;

        public UpdateProductCommandTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
        }

        [Fact]
        public async Task Handle_ValidProduct_ShouldUpdateProduct()
        {
            // Arrange
            var existingProduct = TestHelper.CreateTestProduct();
            var command = new UpdateProductCommand 
            { 
                Id = 1, 
                Name = "Lipitor Updated",
                TradeCode = "LIP123"
            };

            _mockProductRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(existingProduct);

            // Act
            var handler = new UpdateProductCommandHandler(_mockProductRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            _mockProductRepo.Verify(repo => repo.UpdateAsync(It.Is<Product>(p => 
                p.Name == "Lipitor Updated" && 
                p.TradeCode == "LIP123")), Times.Once);
        }
    }
} 