using INUPCO.Catalog.Domain.DTOs;
using INUPCO.Catalog.Domain.Entities.GenericItemPharmas;

namespace INUPCO.Catalog.Domain.Contracts;

/// <summary>
/// Repository interface for Generic Item Pharma operations
/// </summary>
public interface IGenericItemPharmaRepository
{
    Task<GenericItemPharma> CreateAsync(GenericItemPharmaCreateDto dto);
    Task<GenericItemPharma> CreateForMigrationAsync(GenericItemPharmaCreateForMigrationDto dto);
    Task<GenericItemPharma> UpdateForMigrationAsync(GenericItemPharmaUpdateForMigrationDto dto);
    Task<GenericItemPharma?> GetByCodeAsync(string code);
    Task<GenericItemPharma?> GetByIdAsync(int id);
    Task<IEnumerable<string>> GetAllAsync();
    Task UpdateAsync(GenericItemPharma entity);
} 