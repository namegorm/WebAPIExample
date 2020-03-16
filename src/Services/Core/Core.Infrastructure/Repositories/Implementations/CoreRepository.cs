using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

using Core.Domain.Entities.Interfaces;
using Core.Domain.Repositories.Interfaces;
using Core.Infrastructure.Persistence.DatabaseContexts.Interfaces;

using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Repositories.Implementations
{
    public abstract class CoreRepository<TDomainEntity> : ICoreRepository<TDomainEntity>
        where TDomainEntity : class, ICoreDomainEntity, new()
    {
        private readonly ILogger<CoreRepository<TDomainEntity>> _logger;
        protected ICoreDatabaseContext CoreDatabaseContext { get; }

        public CoreRepository(ILogger<CoreRepository<TDomainEntity>> logger, ICoreDatabaseContext coreDatabaseContext)
        {
            _logger = logger;
            CoreDatabaseContext = coreDatabaseContext;
        }

        public virtual IQueryable<TDomainEntity> Get(Expression<Func<TDomainEntity, bool>> expression = default)
        {
            var stopwatch = Stopwatch.StartNew();
            using (_logger.BeginScope($"{nameof(CoreRepository<TDomainEntity>)}.{nameof(Get)}"))
            {
                _logger.LogInformation("Parameters(Expression: {Expression})", expression);
                try
                {
                    var entities = CoreDatabaseContext.Set<TDomainEntity>();
                    return expression == default ? entities : entities.Where(expression);
                }
                finally
                {
                    _logger.LogInformation("Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }

        public virtual TDomainEntity Create(TDomainEntity entity)
        {
            var stopwatch = Stopwatch.StartNew();
            using (_logger.BeginScope($"{nameof(CoreRepository<TDomainEntity>)}.{nameof(Create)}"))
            {
                _logger.LogInformation("Parameters(Entity: {@Entity})", entity);
                try
                {
                    var entityEntry = CoreDatabaseContext.Set<TDomainEntity>().Add(entity);
                    return entityEntry.Entity;
                }
                finally
                {
                    _logger.LogInformation("Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }

        public virtual TDomainEntity Update(long id, TDomainEntity entity)
        {
            var stopwatch = Stopwatch.StartNew();
            using (_logger.BeginScope($"{nameof(CoreRepository<TDomainEntity>)}.{nameof(Update)}"))
            {
                _logger.LogInformation("Parameters(Id: {Id}, Entity: {@Entity})", id, entity);
                try
                {
                    if (CoreDatabaseContext.Set<TDomainEntity>().Any(x => x.Id == id))
                    {
                        entity.Id = id;
                        var entityEntry = CoreDatabaseContext.Set<TDomainEntity>().Update(entity);
                        return entityEntry.Entity;
                    }
                    return null;
                }
                finally
                {
                    _logger.LogInformation("Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }

        public virtual TDomainEntity Delete(long id)
        {
            var stopwatch = Stopwatch.StartNew();
            using (_logger.BeginScope($"{nameof(CoreRepository<TDomainEntity>)}.{nameof(Delete)}"))
            {
                _logger.LogInformation("Parameters(Id: {Id})", id);
                try
                {
                    var entity = CoreDatabaseContext.Set<TDomainEntity>().FirstOrDefault(x => x.Id == id);
                    if (entity != null)
                    {
                        var entityEntry = CoreDatabaseContext.Set<TDomainEntity>().Remove(entity);
                        return entityEntry.Entity;
                    }
                    return null;
                }
                finally
                {
                    _logger.LogInformation("Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }
    }
}
