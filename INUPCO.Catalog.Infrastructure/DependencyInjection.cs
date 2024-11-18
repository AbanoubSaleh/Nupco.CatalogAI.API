using INUPCO.Catalog.Application.Common.Interfaces;
using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Infrastructure.Repositories;
using INUPCO.Catalog.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace INUPCO.Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
        services.AddScoped<ISubsidiaryRepository, SubsidiaryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IExcelProcessor, ExcelProcessor>();
        
        return services;
    }
} 