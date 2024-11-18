using INUPCO.Catalog.Domain.Common;
using INUPCO.Catalog.Domain.Entities.Manufacturers;
using INUPCO.Catalog.Domain.Entities.Products;

namespace INUPCO.Catalog.Domain.Entities.Subsidiaries;

public class Subsidiary : BaseEntity
{
    private readonly List<Product> _products = new();
    
    private Subsidiary() { } // For EF Core

    public string Name { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public int ManufacturerId { get; private set; }
    public Manufacturer Manufacturer { get; private set; } = null!;
    
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public static Subsidiary Create(string name, string country, Manufacturer manufacturer)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be empty", nameof(country));
        if (manufacturer == null)
            throw new ArgumentNullException(nameof(manufacturer));

        var subsidiary = new Subsidiary
        {
            Name = name,
            Country = country,
            Manufacturer = manufacturer,
            ManufacturerId = manufacturer.Id
        };

        manufacturer.AddSubsidiary(subsidiary);
        return subsidiary;
    }

    internal void AddProduct(Product product)
    {
        if (product.ManufacturerId != ManufacturerId)
            throw new InvalidOperationException("Product must belong to the same manufacturer");
            
        _products.Add(product);
    }

    public void UpdateDetails(string name, string country)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be empty", nameof(country));

        Name = name;
        Country = country;
    }
} 