using System.Reflection;

using Core.Infrastructure.Persistence.DatabaseContexts.Implementations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Products.Infrastructure.Persistence.DatabaseContexts.Interfaces;

namespace Products.Infrastructure.Persistence.DatabaseContexts.Implementations
{
    public class DatabaseContext : CoreDatabaseContext, IDatabaseContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override Assembly GetExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
