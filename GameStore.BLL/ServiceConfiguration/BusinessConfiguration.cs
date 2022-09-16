using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.BLL.ServiceConfiguration
{
    public static class BusinessConfiguration
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<IGamesService, GamesService>();
            services.AddTransient<IGenresService, GenresService>();

            services.AddAutoMapper(opt => opt.AddProfile(new AutoMapperProfile()));

            return services;
        }
    }
}
