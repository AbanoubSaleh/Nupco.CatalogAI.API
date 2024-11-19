using INUPCO.Catalog.Domain.Entities.Products;

namespace INUPCO.Catalog.Domain.Contracts;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> GetByTradeCodeAsync(string tradeCode);
    Task<List<Product>> GetByTradeCodesAsync(IEnumerable<string> tradeCodes);
    Task<Product> AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task BulkUpdateAsync(IEnumerable<Product> products);
    Task<bool> ExistsAsync(string tradeCode);
    Task<IEnumerable<Product>> GetByManufacturerIdAsync(int manufacturerId);
    Task<IEnumerable<Product>> GetBySubsidiaryIdAsync(int subsidiaryId);
} 