using INUPCO.Catalog.Domain.Entities.Manufacturers;

namespace INUPCO.Catalog.Domain.Contracts;

public interface IManufacturerRepository
{
    Task<Manufacturer?> GetByIdAsync(int id);
    Task<Manufacturer?> GetByNameAndCountryAsync(string name, string country);
    Task<Manufacturer> AddAsync(Manufacturer manufacturer);
    Task UpdateAsync(Manufacturer manufacturer);
    Task<bool> ExistsAsync(string name, string country);
} 