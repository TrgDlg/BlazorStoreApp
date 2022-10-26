using StoreBlazor.Server.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StoreBlazor.Server.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CnDbContext>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]));

            services.Configure<ConnectionSettings>(configuration.GetSection("ConnectionStrings"));

            services.AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IBlazorShopRepository, BlazorShopRepository>();


            return services;
        }
    }
}