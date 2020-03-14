using System.Reflection;

using AutoMapper;

using Microsoft.Extensions.DependencyInjection;

using Products.Application.ApplicationServices.Implementations;
using Products.Application.ApplicationServices.Interfaces;

namespace Products.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IProductsApplicationService, ProductsApplicationService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
