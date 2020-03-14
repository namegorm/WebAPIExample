using Core.Domain.Entities.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Persistence.Configurations
{
    public abstract class CoreEntityTypeConfiguration<TDomainEntity> : IEntityTypeConfiguration<TDomainEntity>
        where TDomainEntity : class, ICoreDomainEntity
    {
        public virtual void Configure(EntityTypeBuilder<TDomainEntity> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
