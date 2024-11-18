using INUPCO.Catalog.Domain.Entities.Subsidiaries;

namespace INUPCO.Catalog.Domain.Contracts;

public interface ISubsidiaryRepository
{
    Task<Subsidiary?> GetByIdAsync(int id);
    Task<Subsidiary?> GetByNameAndCountryAsync(string name, string country);
    Task<Subsidiary> AddAsync(Subsidiary subsidiary);
    Task UpdateAsync(Subsidiary subsidiary);
    Task<bool> ExistsAsync(string name, string country);
    Task<IEnumerable<Subsidiary>> GetByManufacturerIdAsync(int manufacturerId);
} 