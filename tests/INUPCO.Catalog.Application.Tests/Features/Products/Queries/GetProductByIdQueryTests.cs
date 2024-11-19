using FluentAssertions;
using INUPCO.Catalog.Application.Tests.Common;
using INUPCO.Catalog.Domain.Contracts;
using Moq;
using Xunit;

namespace INUPCO.Catalog.Application.Tests.Features.Products.Queries
{
    public class GetProductByIdQueryTests : TestBase
    {
        private readonly Mock<IProductRepository> _mockProductRepo;

        public GetProductByIdQueryTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
        }

        //[Fact]
        //public async Task Handle_ExistingProduct_ReturnsProductDto()
        //{
        //    // Arrange
        //    var product = TestHelper.CreateTestProduct();
        //    _mockProductRepo.Setup(repo => repo.GetByIdAsync(1))
        //        .ReturnsAsync(product);

        //    // Act
        //    var handler = new GetProductByIdQueryHandler(_mockProductRepo.Object, _mapper);
        //    var result = await handler.Handle(new GetProductByIdQuery(1), CancellationToken.None);

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Name.Should().Be("Lipitor");
        //    result.TradeCode.Should().Be("PFE001");
        //}
    }
} 