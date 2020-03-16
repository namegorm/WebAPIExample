using System.Linq;
using System.Net;

using Core.API.Middleware;
using Core.Application.Models;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Products.Application;
using Products.Application.ViewModels.Implementations;
using Products.Application.ViewModelValidators.Implementations;
using Products.Infrastructure;

using Serilog;

namespace Products.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddApplication();

            services.AddApiVersioning(configuration =>
            {
                configuration.DefaultApiVersion = new ApiVersion(1, 0);
                configuration.AssumeDefaultVersionWhenUnspecified = true;
                configuration.UseApiBehavior = false;
            });

            services.AddTransient<IValidator<ProductViewModel>, ProductViewModelValidator>();
            services.AddControllers();

            services.AddLogging(builder =>
            {
                builder.AddSerilog();
            });

            services.AddMvc().AddFluentValidation().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = x =>
                {
                    var errors = string.Join(' ', x.ModelState.Values.Where(y => y.Errors.Count > 0)
                        .SelectMany(y => y.Errors)
                        .Select(y => y.ErrorMessage));

                    return new OkObjectResult(new CoreResultModel(HttpStatusCode.BadRequest, errors));
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<CoreErrorHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }
    }
}
