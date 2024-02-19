using Spg.AloMalo.Application.Services;
using Spg.AloMalo.DomainModel.Interfaces;

namespace Spg.AloMalo.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDecoratedService<TService, TServiceImplementation, TDecoratorImplemetation>(
            this IServiceCollection services)
            where TService : class
            where TServiceImplementation : class, TService
            where TDecoratorImplemetation : class, TService
        {
            services.AddScoped<TServiceImplementation>();
            //services.AddScoped<TService>(s => new TDecoratorImplemetation(s.GetRequiredService<TServiceImplementation>()));

            return services;
        }
    }
}
