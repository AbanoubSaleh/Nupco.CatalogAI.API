using AutoMapper;
using Microsoft.EntityFrameworkCore;
using INUPCO.Catalog.Infrastructure.Persistence;
using INUPCO.Catalog.Application.Common.Mappings;

namespace INUPCO.Catalog.Application.Tests.Common
{
    public class TestBase
    {
        protected readonly IMapper _mapper;
        protected readonly ApplicationDbContext _dbContext;

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"CatalogDb_{Guid.NewGuid()}")
                .Options;

            _dbContext = new ApplicationDbContext(options);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(MappingProfile).Assembly);
            });
            _mapper = mapperConfig.CreateMapper();
        }
    }
} 