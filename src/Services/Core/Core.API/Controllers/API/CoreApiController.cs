using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Core.Application.ApplicationServices.Interfaces;
using Core.Application.DTOs.Interfaces;
using Core.Application.Models;
using Core.Application.ViewModels.Interfaces;
using Core.Domain.Entities.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.API.Controllers.API
{
    [ApiController]
    [Route("api/v{:apiVersion}/[controller]")]
    public abstract class CoreAPIController<TDomainEntity, TApplicationDTO, TViewModel, TApplicationService> : ControllerBase
        where TDomainEntity : ICoreDomainEntity
        where TApplicationDTO : ICoreApplicationDTO<TDomainEntity>
        where TViewModel : ICoreViewModel
        where TApplicationService : ICoreApplicationService<TDomainEntity, TApplicationDTO, TViewModel>
    {
        private readonly ILogger<CoreAPIController<TDomainEntity, TApplicationDTO, TViewModel, TApplicationService>> _logger;
        protected TApplicationService ApplicationService { get; }

        public CoreAPIController(ILogger<CoreAPIController<TDomainEntity, TApplicationDTO, TViewModel, TApplicationService>> logger,
            TApplicationService applicationService)
        {
            _logger = logger;
            ApplicationService = applicationService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            using (_logger.BeginScope($"{nameof(CoreAPIController<TDomainEntity, TApplicationDTO, TViewModel, TApplicationService>)}.{nameof(GetAsync)}"))
            {
                try
                {
                    var data = await ApplicationService.GetAsync();
                    return Ok(new CoreResultModel(HttpStatusCode.OK, data: data));
                }
                finally
                {
                    _logger.LogInformation("Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAsync(long id)
        {
            var stopwatch = Stopwatch.StartNew();
            using (_logger.BeginScope($"{nameof(CoreAPIController<TDomainEntity, TApplicationDTO, TViewModel, TApplicationService>)}.{nameof(GetAsync)}"))
            {
                _logger.LogInformation("Parameters(Id: {Id})", id);
                try
                {
                    var data = (await ApplicationService.GetAsync(x => x.Id == id)).FirstOrDefault();
                    return Ok(new CoreResultModel(data != null ? HttpStatusCode.OK : HttpStatusCode.NotFound, data: data));
                }
                finally
                {
                    _logger.LogInformation("Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }


        [HttpPost]
        public virtual async Task<IActionResult> PostAsync(TViewModel viewModel)
        {
            var stopwatch = Stopwatch.StartNew();
            using (_logger.BeginScope($"{nameof(CoreAPIController<TDomainEntity, TApplicationDTO, TViewModel, TApplicationService>)}.{nameof(PostAsync)}"))
            {
                _logger.LogInformation("Parameters(ViewModel: {@ViewModel})", viewModel);
                try
                {
                    var data = await ApplicationService.CreateAsync(viewModel);
                    return Ok(new CoreResultModel(data != null ? HttpStatusCode.Created : HttpStatusCode.BadRequest, data: data));
                }
                finally
                {
                    _logger.LogInformation("Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> PutAsync(long id, TViewModel viewModel)
        {
            var stopwatch = Stopwatch.StartNew();
            using (_logger.BeginScope($"{nameof(CoreAPIController<TDomainEntity, TApplicationDTO, TViewModel, TApplicationService>)}.{nameof(PutAsync)}"))
            {
                _logger.LogInformation("Parameters(Id: {Id}, ViewModel: {@ViewModel})", id, viewModel);
                try
                {
                    var data = await ApplicationService.UpdateAsync(id, viewModel);
                    return Ok(new CoreResultModel(data != null ? HttpStatusCode.OK : HttpStatusCode.NotFound, data: data));
                }
                finally
                {
                    _logger.LogInformation("Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(long id)
        {
            var stopwatch = Stopwatch.StartNew();
            using (_logger.BeginScope($"{nameof(CoreAPIController<TDomainEntity, TApplicationDTO, TViewModel, TApplicationService>)}.{nameof(DeleteAsync)}"))
            {
                _logger.LogInformation("Parameters(Id: {Id})", id);
                try
                {
                    var data = await ApplicationService.DeleteAsync(id);
                    return Ok(new CoreResultModel(data != null ? HttpStatusCode.OK : HttpStatusCode.NotFound, data: data));
                }
                finally
                {
                    _logger.LogInformation("Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }
    }
}
