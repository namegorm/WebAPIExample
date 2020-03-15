using Microsoft.Extensions.DependencyInjection;

using Products.Domain.Repositories.Interfaces;
using Products.Domain.UnitOfWorks.Interfaces;
using Products.Infrastructure.Persistence.DatabaseContexts.Implementations;
using Products.Infrastructure.Persistence.DatabaseContexts.Interfaces;
using Products.Infrastructure.Repositories.Implementations;
using Products.Infrastructure.UnitOfWorks.Implementations;

namespace Products.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseContext, DatabaseContext>();
            services.AddScoped<IDatabaseContext>(x => x.GetService<DatabaseContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<DatabaseContext>();
            services.AddTransient<IProductsRepository, ProductsRepository>();

            return services;
        }
    }
}
