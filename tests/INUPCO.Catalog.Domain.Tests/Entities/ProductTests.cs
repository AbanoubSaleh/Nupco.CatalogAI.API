using Xunit;
using FluentAssertions;
using INUPCO.Catalog.Domain.Entities.Products;
using INUPCO.Catalog.Domain.Entities;
using INUPCO.Catalog.Domain.Entities.Manufacturers;
using INUPCO.Catalog.Domain.Entities.Subsidiaries;
using System;
using Moq;
using INUPCO.Catalog.Domain.Contracts;

namespace INUPCO.Catalog.Domain.Tests.Entities
{
    public class ProductTests
    {
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly Mock<IManufacturerRepository> _manufacturerRepository;

        public ProductTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _manufacturerRepository = new Mock<IManufacturerRepository>();
        }

        private static Manufacturer CreateTestManufacturer(
            string name = "Pfizer", 
            string country = "USA")
        {
            return Manufacturer.Create(name, country);
        }

        private static Subsidiary CreateTestSubsidiary(
            string name = "Pfizer UK",
            string country = "UK",
            Manufacturer? manufacturer = null)
        {
            manufacturer ??= CreateTestManufacturer();
            return Subsidiary.Create(name, country, manufacturer);
        }

        [Fact]
        public void Create_WithValidInputs_ShouldCreateProduct()
        {
            // Arrange
            var manufacturer = CreateTestManufacturer();
            var name = "Lipitor";
            var tradeCode = "PFE001";

            // Act
            var product = Product.Create(name, tradeCode, manufacturer);

            // Assert
            product.Should().NotBeNull();
            product.Name.Should().Be(name);
            product.TradeCode.Should().Be(tradeCode);
            product.ManufacturerId.Should().Be(manufacturer.Id);
            product.Manufacturer.Should().Be(manufacturer);
            product.SubsidiaryId.Should().BeNull();
        }

        [Fact]
        public void Create_WithSubsidiary_ShouldCreateProductWithSubsidiary()
        {
            // Arrange
            var manufacturer = CreateTestManufacturer();
            var subsidiary = CreateTestSubsidiary();
            var name = "Lipitor";
            var tradeCode = "PFE001";

            // Act
            var product = Product.Create(name, tradeCode, manufacturer, subsidiary);

            // Assert
            product.Should().NotBeNull();
            product.SubsidiaryId.Should().Be(subsidiary.Id);
            product.Subsidiary.Should().Be(subsidiary);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_WithInvalidName_ShouldThrowArgumentException(string invalidName)
        {
            // Arrange
            var manufacturer = CreateTestManufacturer();

            // Act
            var act = () => Product.Create(invalidName, "PFE001", manufacturer);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Name cannot be empty*");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_WithInvalidTradeCode_ShouldThrowArgumentException(string invalidTradeCode)
        {
            // Arrange
            var manufacturer = CreateTestManufacturer();

            // Act
            var act = () => Product.Create("Lipitor", invalidTradeCode, manufacturer);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Trade code cannot be empty*");
        }

        [Fact]
        public void Create_WithNullManufacturer_ShouldThrowArgumentNullException()
        {
            // Act
            var act = () => Product.Create("Lipitor", "PFE001", null);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("manufacturer");
        }
    }
} 