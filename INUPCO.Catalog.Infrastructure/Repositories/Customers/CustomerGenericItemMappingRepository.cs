using INUPCO.Catalog.Domain.Contracts.Customers;
using INUPCO.Catalog.Domain.Entities.Customers;
using INUPCO.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace INUPCO.Catalog.Infrastructure.Repositories.Customers;

public class CustomerGenericItemMappingRepository : ICustomerGenericItemMappingRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerGenericItemMappingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerGenericItemPharmaCodeMapping?> GetByCustomerAndGenericCodeAsync(string customerCode, string genericCode)
    {
        return await _context.CustomerGenericItemPharmaCodeMappings
            .FirstOrDefaultAsync(x => x.CustomerCode == customerCode && x.GenericItemPharma.Code == genericCode);
    }

    public async Task<IEnumerable<CustomerGenericItemPharmaCodeMapping>> GetByGenericItemIdAsync(int genericItemId)
    {
        return await _context.CustomerGenericItemPharmaCodeMappings
            .Where(x => x.GenericItemPharmaId == genericItemId)
            .ToListAsync();
    }

    public async Task<CustomerGenericItemPharmaCodeMapping> AddAsync(CustomerGenericItemPharmaCodeMapping mapping)
    {
        await _context.CustomerGenericItemPharmaCodeMappings.AddAsync(mapping);
        await _context.SaveChangesAsync();
        return mapping;
    }

    public async Task<bool> ExistsAsync(string customerCode, string customerSpecificCode)
    {
        return await _context.CustomerGenericItemPharmaCodeMappings
            .AnyAsync(x => x.CustomerCode == customerCode && x.CustomerSpecificCode == customerSpecificCode);
    }
} 