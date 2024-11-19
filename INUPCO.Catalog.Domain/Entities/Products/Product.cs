using INUPCO.Catalog.Domain.Common;
using INUPCO.Catalog.Domain.Entities.Manufacturers;
using INUPCO.Catalog.Domain.Entities.Subsidiaries;

namespace INUPCO.Catalog.Domain.Entities.Products;

public class Product : BaseEntity
{
    private Product() { } // For EF Core

    public string Name { get; private set; } = string.Empty;
    public string TradeCode { get; private set; } = string.Empty;
    
    public int ManufacturerId { get; private set; }
    public Manufacturer? Manufacturer { get; private set; }
    
    public int? SubsidiaryId { get; private set; }
    public Subsidiary? Subsidiary { get; private set; }

    public void AssignToManufacturer(int manufacturerId)
    {
        ManufacturerId = manufacturerId;
        SubsidiaryId = null;
        Subsidiary = null;
    }

    public void AssignToSubsidiary(Subsidiary subsidiary)
    {
        if (subsidiary == null)
            throw new ArgumentNullException(nameof(subsidiary));

        SubsidiaryId = subsidiary.Id;
        Subsidiary = subsidiary;
        ManufacturerId = subsidiary.ManufacturerId;
        Manufacturer = subsidiary.Manufacturer;
    }

    public static Product Create(
        string name, 
        string tradeCode, 
        Manufacturer manufacturer,
        Subsidiary? subsidiary = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(tradeCode))
            throw new ArgumentException("Trade code cannot be empty", nameof(tradeCode));
        if (manufacturer == null)
            throw new ArgumentNullException(nameof(manufacturer));

        var product = new Product
        {
            Name = name,
            TradeCode = tradeCode,
            Manufacturer = manufacturer,
            ManufacturerId = manufacturer.Id,
            Subsidiary = subsidiary,
            SubsidiaryId = subsidiary?.Id
        };

        return product;
    }

    public void UpdateDetails(string name, string tradeCode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(tradeCode))
            throw new ArgumentException("Trade code cannot be empty", nameof(tradeCode));

        Name = name;
        TradeCode = tradeCode;
    }
} 