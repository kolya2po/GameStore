using System.Text;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.DAL;
using GameStore.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.BLL.ServiceConfiguration
{
    public static class BusinessConfiguration
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<IGamesService, GamesService>();
            services.AddTransient<IGenresService, GenresService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IImagesService, ImagesService>();
            services.AddTransient<ICartsService, CartsService>();
            services.AddTransient<ICommentsService, CommentsService>();

            services.AddAutoMapper(opt => opt.AddProfile(new AutoMapperProfile()));

            return services;
        }

        public static IServiceCollection AddAndConfigureIdentityWithJwtToken(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<JwtHandler>();

            services.AddIdentity<User, IdentityRole<int>>(config =>
                {
                    config.User.RequireUniqueEmail = true;
                    config.Password.RequireDigit = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<GameStoreDbContext>();

            var tokenConfiguration = configuration.GetSection("JWT");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = tokenConfiguration["validAudience"],

                    ValidateIssuer = true,
                    ValidIssuer = tokenConfiguration["validIssuer"],

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (
                        Encoding.UTF8.GetBytes(tokenConfiguration["secret"])
                    )
                };
            });


            return services;
        }
    }
}
