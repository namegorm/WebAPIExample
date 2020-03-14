using System;
using System.Reflection;

using Core.Infrastructure.Persistence.DatabaseContexts.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.Infrastructure.Persistence.DatabaseContexts.Implementations
{
    public class CoreDatabaseContext : DbContext, ICoreDatabaseContext
    {
        protected IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected virtual Assembly GetExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
