using System;
using System.Linq;
using System.Linq.Expressions;

using Core.Domain.Entities.Interfaces;

namespace Core.Domain.Repositories.Interfaces
{
    public interface ICoreRepository<TDomainEntity>
        where TDomainEntity : ICoreDomainEntity
    {
        IQueryable<TDomainEntity> Get(Expression<Func<TDomainEntity, bool>> expression);
        TDomainEntity Create(TDomainEntity entity);
        TDomainEntity Update(long id, TDomainEntity entity);
        TDomainEntity Delete(long id);
    }
}
