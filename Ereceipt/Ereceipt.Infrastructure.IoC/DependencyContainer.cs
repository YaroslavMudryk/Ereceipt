using Ereceipt.Application.Services.Implementations;
using Ereceipt.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Ereceipt.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddUSNServices(this IServiceCollection services)
        {
            services.AddSingleton<ITokenManager, InMemoryTokenManager>();

            return services;
        }
    }
}