using Microsoft.Extensions.DependencyInjection;
using Wood.Application.Common.Interfaces;
using Wood.Infrastructure.Persistence;
using Wood.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Wood.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    string? connectionString = null)
        {
            if ( string.IsNullOrEmpty(connectionString) )
            {
                services.AddDbContext<AppDbContext>(opt =>
                    opt.UseInMemoryDatabase("LumberRivneDb"));
            }
            else
            {
                services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(connectionString));
            }

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
