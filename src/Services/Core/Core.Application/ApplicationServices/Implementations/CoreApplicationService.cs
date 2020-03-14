using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;

using Core.Application.ApplicationServices.Interfaces;
using Core.Application.DTOs.Interfaces;
using Core.Domain.Entities.Interfaces;
using Core.Domain.Repositories.Interfaces;
using Core.Domain.UnitOfWorks.Interfaces;

namespace Core.Application.ApplicationServices.Implementations
{
    public abstract class CoreApplicationService<TDomainEntity, TApplicationDTO, TViewModel, TUnitOfWork, TRepository> : ICoreApplicationService<TDomainEntity, TApplicationDTO, TViewModel>
        where TDomainEntity : ICoreDomainEntity
        where TApplicationDTO : ICoreApplicationDTO<TDomainEntity>
        where TUnitOfWork : ICoreUnitOfWork
        where TRepository : class, ICoreRepository<TDomainEntity>
    {
        protected ICoreUnitOfWork UnitOfWork { get; }
        protected TRepository Repository { get; }
        protected IMapper Mapper { get; }

        public CoreApplicationService(TUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Repository = unitOfWork.GetRepository<TRepository>();
            Mapper = mapper;
        }

        public virtual async Task<IEnumerable<TApplicationDTO>> GetAsync(Expression<Func<TDomainEntity, bool>> expression = null)
        {
            return Repository.Get(expression).ToList().Select(x => Mapper.Map<TDomainEntity, TApplicationDTO>(x));
        }

        public virtual async Task<TApplicationDTO> CreateAsync(TViewModel viewModel)
        {
            var entity = Mapper.Map<TViewModel, TDomainEntity>(viewModel);
            entity = Repository.Create(entity);
            await UnitOfWork.CommitAsync();
            return Mapper.Map<TDomainEntity, TApplicationDTO>(entity);
        }

        public virtual async Task<TApplicationDTO> UpdateAsync(TViewModel viewModel)
        {
            var entity = Mapper.Map<TViewModel, TDomainEntity>(viewModel);
            entity = Repository.Update(entity);
            await UnitOfWork.CommitAsync();
            return Mapper.Map<TDomainEntity, TApplicationDTO>(entity);
        }

        public virtual async Task<TApplicationDTO> DeleteAsync(long id)
        {
            var entity = Repository.Delete(id);
            await UnitOfWork.CommitAsync();
            return Mapper.Map<TDomainEntity, TApplicationDTO>(entity);
        }
    }
}
