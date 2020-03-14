using System;
using System.Linq;
using System.Linq.Expressions;

using Core.Domain.Entities.Interfaces;
using Core.Domain.Repositories.Interfaces;
using Core.Infrastructure.Persistence.DatabaseContexts.Interfaces;

namespace Core.Infrastructure.Repositories.Implementations
{
    public abstract class CoreRepository<TDomainEntity> : ICoreRepository<TDomainEntity>
        where TDomainEntity : class, ICoreDomainEntity, new()
    {
        protected ICoreDatabaseContext CoreDatabaseContext { get; }

        public CoreRepository(ICoreDatabaseContext coreDatabaseContext)
        {
            CoreDatabaseContext = coreDatabaseContext;
        }

        public virtual IQueryable<TDomainEntity> Get(Expression<Func<TDomainEntity, bool>> expression = default)
        {
            var entities = CoreDatabaseContext.Set<TDomainEntity>();
            return expression == default ? entities : entities.Where(expression);
        }

        public virtual TDomainEntity Create(TDomainEntity entity)
        {
            var entityEntry = CoreDatabaseContext.Set<TDomainEntity>().Add(entity);
            return entityEntry.Entity;
        }

        public virtual TDomainEntity Update(TDomainEntity entity)
        {
            var entityEntry = CoreDatabaseContext.Set<TDomainEntity>().Update(entity);
            return entityEntry.Entity;
        }

        public virtual TDomainEntity Delete(long id)
        {
            var entityEntry = CoreDatabaseContext.Set<TDomainEntity>().Remove(new TDomainEntity { Id = id });
            return entityEntry.Entity;
        }
    }
}
