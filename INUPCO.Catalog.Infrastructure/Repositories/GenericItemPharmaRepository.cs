using DocumentFormat.OpenXml.Office2010.Excel;
using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Domain.DTOs;
using INUPCO.Catalog.Domain.Entities.GenericItemPharmas;
using INUPCO.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace INUPCO.Catalog.Infrastructure.Repositories;

/// <summary>
/// Implementation of Generic Item Pharma repository
/// </summary>
public class GenericItemPharmaRepository : IGenericItemPharmaRepository
{
    private readonly ApplicationDbContext _context;

    public GenericItemPharmaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GenericItemPharma> CreateAsync(GenericItemPharmaCreateDto dto)
    {
        var entity = GenericItemPharma.Create(dto.Code, dto.Name, dto.Description, dto.CustomerCode);
        _context.GenericItemPharmas.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public Task<GenericItemPharma> CreateForMigrationAsync(GenericItemPharmaCreateForMigrationDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<string>> GetAllAsync()
    {
        return await _context.GenericItemPharmas
            .Select(x => x.Code).ToListAsync();
    }

    public async Task<GenericItemPharma?> GetByCodeAsync(string code)
    {
        return await _context.GenericItemPharmas
            .FirstOrDefaultAsync(x => x.Code == code);    }

    public async Task<GenericItemPharma?> GetByIdAsync(int id)
    {
        return await _context.GenericItemPharmas
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(GenericItemPharma entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public Task<GenericItemPharma> UpdateForMigrationAsync(GenericItemPharmaUpdateForMigrationDto dto)
    {
        throw new NotImplementedException();
    }

    // Implement other interface methods...
}