using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;

using Core.Application.ApplicationServices.Interfaces;
using Core.Application.DTOs.Interfaces;
using Core.Domain.Entities.Interfaces;
using Core.Domain.Repositories.Interfaces;
using Core.Domain.UnitOfWorks.Interfaces;

using Microsoft.Extensions.Logging;

namespace Core.Application.ApplicationServices.Implementations
{
    public abstract class CoreApplicationService<TDomainEntity, TApplicationDTO, TViewModel, TUnitOfWork, TRepository> : ICoreApplicationService<TDomainEntity, TApplicationDTO, TViewModel>
        where TDomainEntity : ICoreDomainEntity
        where TApplicationDTO : ICoreApplicationDTO<TDomainEntity>
        where TUnitOfWork : ICoreUnitOfWork
        where TRepository : class, ICoreRepository<TDomainEntity>
    {
        private readonly ILogger<CoreApplicationService<TDomainEntity, TApplicationDTO, TViewModel, TUnitOfWork, TRepository>> _logger;
        protected ICoreUnitOfWork UnitOfWork { get; }
        protected TRepository Repository { get; }
        protected IMapper Mapper { get; }

        public CoreApplicationService(ILogger<CoreApplicationService<TDomainEntity, TApplicationDTO, TViewModel, TUnitOfWork, TRepository>> logger, TUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            UnitOfWork = unitOfWork;
            Repository = unitOfWork.GetRepository<TRepository>();
            Mapper = mapper;
        }

        public virtual async Task<IEnumerable<TApplicationDTO>> GetAsync(Expression<Func<TDomainEntity, bool>> expression = null)
        {
            var stopwatch = Stopwatch.StartNew();
            using (_logger.BeginScope($"{nameof(CoreApplicationService<TDomainEntity, TApplicationDTO, TViewModel, TUnitOfWork, TRepository>)}.{nameof(GetAsync)}"))
            {
                _logger.LogInformation("Parameters(Expression: {Expression})", expression);
                try
                {
                    return Repository.Get(expression).ToList().Select(x => Mapper.Map<TDomainEntity, TApplicationDTO>(x));
                }
                finally
                {
                    _logger.LogInformation("Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }

        public virtual async Task<TApplicationDTO> CreateAsync(TViewModel viewModel)
        {
            var stopwatch = Stopwatch.StartNew();
            using (_logger.BeginScope($"{nameof(CoreApplicationService<TDomainEntity, TApplicationDTO, TViewModel, TUnitOfWork, TRepository>)}.{nameof(CreateAsync)}"))
            {
                _logger.LogInformation("Parameters(ViewModel: {@ViewModel})", viewModel);
                try
                {
                    var entity = Mapper.Map<TViewModel, TDomainEntity>(viewModel);
                    entity = Repository.Create(entity);
                    await UnitOfWork.CommitAsync();
                    _logger.LogInformation("The record has been created. {@Entity}", entity);
                    return Mapper.Map<TDomainEntity, TApplicationDTO>(entity);
                }
                finally
                {
                    _logger.LogInformation("Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }

        public virtual async Task<TApplicationDTO> UpdateAsync(long id, TViewModel viewModel)
        {
            var stopwatch = Stopwatch.StartNew();
            using (_logger.BeginScope($"{nameof(CoreApplicationService<TDomainEntity, TApplicationDTO, TViewModel, TUnitOfWork, TRepository>)}.{nameof(UpdateAsync)}"))
            {
                _logger.LogInformation("Parameters(Id: {Id}, ViewModel: {@ViewModel})", id, viewModel);
                try
                {
                    var entity = Mapper.Map<TViewModel, TDomainEntity>(viewModel);
                    entity = Repository.Update(id, entity);
                    await UnitOfWork.CommitAsync();
                    _logger.LogInformation("The record has been updated. {@Entity}", entity);
                    return Mapper.Map<TDomainEntity, TApplicationDTO>(entity);
                }
                finally
                {
                    _logger.LogInformation("Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }

        public virtual async Task<TApplicationDTO> DeleteAsync(long id)
        {
            var stopwatch = Stopwatch.StartNew();
            using (_logger.BeginScope($"{nameof(CoreApplicationService<TDomainEntity, TApplicationDTO, TViewModel, TUnitOfWork, TRepository>)}.{nameof(DeleteAsync)}"))
            {
                _logger.LogInformation("Parameters(Id: {Id})", id);
                try
                {
                    var entity = Repository.Delete(id);
                    await UnitOfWork.CommitAsync();
                    _logger.LogInformation("The record has been deleted. {@Entity}", entity);
                    return Mapper.Map<TDomainEntity, TApplicationDTO>(entity);
                }
                finally
                {
                    _logger.LogInformation("Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }
    }
}
