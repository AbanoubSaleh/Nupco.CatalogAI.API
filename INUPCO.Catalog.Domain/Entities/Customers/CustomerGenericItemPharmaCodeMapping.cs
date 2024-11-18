using INUPCO.Catalog.Domain.Common;
using INUPCO.Catalog.Domain.Entities.GenericItemPharmas;

namespace INUPCO.Catalog.Domain.Entities.Customers;

public class CustomerGenericItemPharmaCodeMapping : BaseEntity
{
    private CustomerGenericItemPharmaCodeMapping() { } // For EF Core

    internal CustomerGenericItemPharmaCodeMapping(string customerCode, string customerSpecificCode, GenericItemPharma genericItemPharma)
    {
        CustomerCode = customerCode;
        CustomerSpecificCode = customerSpecificCode;
        GenericItemPharma = genericItemPharma;
        GenericItemPharmaId = genericItemPharma.Id;
    }

    public string CustomerCode { get; private set; } = string.Empty;
    public string CustomerSpecificCode { get; private set; } = string.Empty;
    public int GenericItemPharmaId { get; private set; }
    public GenericItemPharma GenericItemPharma { get; private set; } = null!;

    public static CustomerGenericItemPharmaCodeMapping Create(string customerCode, string customerSpecificCode, GenericItemPharma genericItemPharma)
    {
        if (string.IsNullOrWhiteSpace(customerCode))
            throw new ArgumentException("Customer code cannot be empty", nameof(customerCode));
        if (string.IsNullOrWhiteSpace(customerSpecificCode))
            throw new ArgumentException("Customer specific code cannot be empty", nameof(customerSpecificCode));
        if (genericItemPharma == null)
            throw new ArgumentNullException(nameof(genericItemPharma));

        return new CustomerGenericItemPharmaCodeMapping(customerCode, customerSpecificCode, genericItemPharma);
    }
} 