using INUPCO.Catalog.Domain.Entities.Products;

namespace INUPCO.Catalog.Domain.Contracts;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> GetByTradeCodeAsync(string tradeCode);
    Task<Product> AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task<bool> ExistsAsync(string tradeCode);
    Task<IEnumerable<Product>> GetByManufacturerIdAsync(int manufacturerId);
    Task<IEnumerable<Product>> GetBySubsidiaryIdAsync(int subsidiaryId);
} 