using INUPCO.Catalog.Domain.Entities.Products;
using INUPCO.Catalog.Domain.Entities.Manufacturers;

namespace INUPCO.Catalog.Application.Tests.Common;

public static class TestHelper
{
    public static Product CreateTestProduct(
        string name = "Lipitor",
        string tradeCode = "PFE001",
        Manufacturer? manufacturer = null)
    {
        manufacturer ??= CreateTestManufacturer();
        return Product.Create(name, tradeCode, manufacturer);
    }

    public static Manufacturer CreateTestManufacturer(
        string name = "Pfizer",
        string country = "USA")
    {
        return Manufacturer.Create(name, country);
    }
} 