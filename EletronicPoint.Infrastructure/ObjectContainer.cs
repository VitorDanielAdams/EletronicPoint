using EletronicPoint.Application.Repositories;
using EletronicPoint.Application.Repositories.Common;
using EletronicPoint.Application.Services.Impl;
using EletronicPoint.Application.Services.Interfaces;
using EletronicPoint.Infrastructure.Repositories;
using EletronicPoint.Infrastructure.Repositories.Common;
using EletronicPoint.Infrastructure.Service.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EletronicPoint.Infrastructure
{
    public static class ObjectContainer
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            #region Repository
            // Generic Repository
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ITokenService, TokenService>();

            #endregion

            return services;
        }
    }
}
