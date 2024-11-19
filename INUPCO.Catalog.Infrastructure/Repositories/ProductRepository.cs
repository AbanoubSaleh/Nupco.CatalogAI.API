using EFCore.BulkExtensions;
using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Domain.Entities.Products;
using INUPCO.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace INUPCO.Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(x => x.Manufacturer)
            .Include(x => x.Subsidiary)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Product?> GetByTradeCodeAsync(string tradeCode)
    {
        return await _context.Products
            .Include(x => x.Manufacturer)
            .Include(x => x.Subsidiary)
            .FirstOrDefaultAsync(x => x.TradeCode == tradeCode);
    }

    public async Task<Product> AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string tradeCode)
    {
        return await _context.Products
            .AnyAsync(x => x.TradeCode == tradeCode);
    }

    public async Task<IEnumerable<Product>> GetByManufacturerIdAsync(int manufacturerId)
    {
        return await _context.Products
            .Include(x => x.Manufacturer)
            .Include(x => x.Subsidiary)
            .Where(x => x.ManufacturerId == manufacturerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetBySubsidiaryIdAsync(int subsidiaryId)
    {
        return await _context.Products
            .Include(x => x.Manufacturer)
            .Include(x => x.Subsidiary)
            .Where(x => x.SubsidiaryId == subsidiaryId)
            .ToListAsync();
    }

    public async Task<Dictionary<string, Product>> GetByTradeCodesAsync(IEnumerable<string> tradeCodes)
    {
        var products = await _context.Products
            .Include(x => x.Manufacturer)
            .Include(x => x.Subsidiary)
            .Where(x => tradeCodes.Contains(x.TradeCode))
            .ToListAsync();

        return products.ToDictionary(p => p.TradeCode, p => p);
    }

    public async Task BulkUpdateAsync(IEnumerable<Product> products)
    {
        // Remove duplicates by ID
        var distinctProducts = products
            .GroupBy(p => p.Id)
            .Select(g => g.First())
            .ToList();

        var config = new BulkConfig 
        { 
            PreserveInsertOrder = false,
            SetOutputIdentity = false,
            BatchSize = 100,
            UseTempDB = false,
            EnableShadowProperties = true
        };
        
        await _context.BulkUpdateAsync(distinctProducts, config);
        await _context.SaveChangesAsync();
    }
} 