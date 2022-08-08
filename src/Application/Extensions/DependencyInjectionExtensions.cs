using Domain.ShapeAggregate;
using Infrastructure.Repositories;

namespace Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructures(this IServiceCollection services)
        {
            services.AddScoped<IShapeRepository, ShapeRepository>();

            return services;
        }
    }
}