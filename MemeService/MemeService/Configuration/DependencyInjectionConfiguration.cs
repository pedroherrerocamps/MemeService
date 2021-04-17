using MemeService.Services.Meme.Memes;
using MemeService.Services.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeService.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependecyInjectionConfiguration(this IServiceCollection services)
        {
            services
                .AddScoped<IMemeRepository, MemeRepository>()
                .AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
