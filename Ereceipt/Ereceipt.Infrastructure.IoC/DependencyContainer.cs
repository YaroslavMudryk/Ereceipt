using AutoMapper;
using Ereceipt.Application.Profiles;
using Ereceipt.Application.Services.Implementations;
using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Infrastructure.Data.EntityFramework.Context;
using Extensions.DeviceDetector;
using Microsoft.Extensions.DependencyInjection;

namespace Ereceipt.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddUSNServices(this IServiceCollection services)
        {
            services.AddDeviceDetector();

            services.AddHttpContextAccessor();

            services.AddMapperProfiles();

            services.AddSqlServer<EreceiptContext>("Server=(localdb)\\MSSQLLocalDB;Database=EreceiptDb;Trusted_Connection=True;");

            services.AddSingleton<ITokenManager, InMemoryTokenManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDetectClient, DetectClient>();
            services.AddScoped<IClaimsProvider, ClaimsProvider>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ISessionService, SessionService>();

            return services;
        }

        private static IServiceCollection AddMapperProfiles(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UsersProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}