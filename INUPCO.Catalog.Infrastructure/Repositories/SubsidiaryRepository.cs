using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Domain.Entities.Subsidiaries;
using INUPCO.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace INUPCO.Catalog.Infrastructure.Repositories;

public class SubsidiaryRepository : ISubsidiaryRepository
{
    private readonly ApplicationDbContext _context;

    public SubsidiaryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Subsidiary?> GetByIdAsync(int id)
    {
        return await _context.Subsidiaries
            .Include(x => x.Products)
            .Include(x => x.Manufacturer)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Subsidiary?> GetByNameAndCountryAsync(string name, string country)
    {
        return await _context.Subsidiaries
            .FirstOrDefaultAsync(x => x.Name == name && x.Country == country);
    }

    public async Task<Subsidiary> AddAsync(Subsidiary subsidiary)
    {
        await _context.Subsidiaries.AddAsync(subsidiary);
        await _context.SaveChangesAsync();
        return subsidiary;
    }

    public async Task UpdateAsync(Subsidiary subsidiary)
    {
        _context.Entry(subsidiary).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string name, string country)
    {
        return await _context.Subsidiaries
            .AnyAsync(x => x.Name == name && x.Country == country);
    }

    public async Task<IEnumerable<Subsidiary>> GetByManufacturerIdAsync(int manufacturerId)
    {
        return await _context.Subsidiaries
            .Include(x => x.Products)
            .Where(x => x.ManufacturerId == manufacturerId)
            .ToListAsync();
    }
} 