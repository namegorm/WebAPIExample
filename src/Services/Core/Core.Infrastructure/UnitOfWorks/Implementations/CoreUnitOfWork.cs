using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Core.Domain.UnitOfWorks.Interfaces;
using Core.Infrastructure.Persistence.DatabaseContexts.Interfaces;

namespace Core.Infrastructure.UnitOfWorks.Implementations
{
    public class CoreUnitOfWork : ICoreUnitOfWork
    {
        protected ICoreDatabaseContext CoreDatabaseContext { get; }
        protected IDictionary<Type, object> Repositories { get; set; }

        public CoreUnitOfWork(ICoreDatabaseContext coreDatabaseContext)
        {
            CoreDatabaseContext = coreDatabaseContext;
        }

        public virtual async Task<int> CommitAsync()
        {
            var saveChangesAsyncResult = await CoreDatabaseContext.SaveChangesAsync();
            return saveChangesAsyncResult;
        }

        public virtual TRepository GetRepository<TRepository>()
            where TRepository : class
        {
            var repository = (TRepository)Repositories[typeof(TRepository)];
            return repository;
        }
    }
}
