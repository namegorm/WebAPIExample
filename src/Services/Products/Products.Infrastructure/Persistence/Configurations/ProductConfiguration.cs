using Core.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Products.Domain.Entities.Implementations;

namespace Products.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : CoreEntityTypeConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
        }
    }
}
