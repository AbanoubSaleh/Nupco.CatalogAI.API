using INUPCO.Catalog.Domain.Common;
using INUPCO.Catalog.Domain.Entities.Products;
using INUPCO.Catalog.Domain.Entities.Subsidiaries;

namespace INUPCO.Catalog.Domain.Entities.Manufacturers;

public class Manufacturer : BaseEntity
{
    private readonly List<Product> _products = new();
    private readonly List<Subsidiary> _subsidiaries = new();
    
    private Manufacturer() { } // For EF Core

    public string Name { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
    public IReadOnlyCollection<Subsidiary> Subsidiaries => _subsidiaries.AsReadOnly();

    public static Manufacturer Create(string name, string country)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be empty", nameof(country));

        return new Manufacturer
        {
            Name = name,
            Country = country
        };
    }

    internal void AddProduct(Product product)
    {
        _products.Add(product);
    }

    internal void AddSubsidiary(Subsidiary subsidiary)
    {
        _subsidiaries.Add(subsidiary);
    }
} 