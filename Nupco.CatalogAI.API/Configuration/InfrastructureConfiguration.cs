using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Nupco.CatalogAI.API.Configuration;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
        services.AddScoped<ISubsidiaryRepository, SubsidiaryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        
        return services;
    }
} 