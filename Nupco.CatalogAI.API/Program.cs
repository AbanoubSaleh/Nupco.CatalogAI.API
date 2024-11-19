using INUPCO.Catalog.Application.Common.Interfaces;
using INUPCO.Catalog.Application.Common.Mappings;
using INUPCO.Catalog.Application.Features.GenericItemPharmas.Commands.CreateGenericItem;
using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Infrastructure.Persistence;
using INUPCO.Catalog.Infrastructure.Repositories;
using INUPCO.Catalog.Infrastructure.Repositories.Customers;
using INUPCO.Catalog.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Nupco.CatalogAI.API.Configuration;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IGenericItemPharmaRepository, GenericItemPharmaRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(typeof(CreateGenericItemCommand).Assembly);
});

builder.Services.AddScoped<IExcelProcessor, ExcelProcessor>();
builder.Services.AddScoped<INUPCO.Catalog.Domain.Contracts.Customers.ICustomerGenericItemMappingRepository, CustomerGenericItemMappingRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
