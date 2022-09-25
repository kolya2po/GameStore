using GameStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.DAL.ServiceConfiguration
{
    public static class DataConfiguration
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<GameStoreDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddDbContext<GameStoreDbContext>(opt =>
                opt.UseInMemoryDatabase("db1"));
                

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
