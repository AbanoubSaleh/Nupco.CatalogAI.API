using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Domain.Entities.Manufacturers;
using INUPCO.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace INUPCO.Catalog.Infrastructure.Repositories;

public class ManufacturerRepository : IManufacturerRepository
{
    private readonly ApplicationDbContext _context;

    public ManufacturerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Manufacturer?> GetByIdAsync(int id)
    {
        return await _context.Manufacturers
            .Include(x => x.Products)
            .Include(x => x.Subsidiaries)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Manufacturer?> GetByNameAndCountryAsync(string name, string country)
    {
        return await _context.Manufacturers
            .FirstOrDefaultAsync(x => x.Name == name && x.Country == country);
    }

    public async Task<Manufacturer> AddAsync(Manufacturer manufacturer)
    {
        await _context.Manufacturers.AddAsync(manufacturer);
        await _context.SaveChangesAsync();
        return manufacturer;
    }

    public async Task UpdateAsync(Manufacturer manufacturer)
    {
        _context.Entry(manufacturer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string name, string country)
    {
        return await _context.Manufacturers
            .AnyAsync(x => x.Name == name && x.Country == country);
    }
} 