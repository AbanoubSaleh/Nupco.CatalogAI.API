using INUPCO.Catalog.Domain.Entities;
using INUPCO.Catalog.Domain.Entities.Customers;

namespace INUPCO.Catalog.Domain.Contracts;

public interface ICustomerGenericItemMappingRepository
{
    Task<CustomerGenericItemPharmaCodeMapping?> GetByCustomerAndGenericCodeAsync(string customerCode, string genericCode);
    Task<IEnumerable<CustomerGenericItemPharmaCodeMapping>> GetByGenericItemIdAsync(int genericItemId);
    Task<CustomerGenericItemPharmaCodeMapping> AddAsync(CustomerGenericItemPharmaCodeMapping mapping);
    Task<bool> ExistsAsync(string customerCode, string customerSpecificCode);
} 