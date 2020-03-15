using System.Net;
using System.Threading.Tasks;

using Core.Application.ApplicationServices.Interfaces;
using Core.Application.DTOs.Interfaces;
using Core.Application.Models;
using Core.Application.ViewModels.Interfaces;
using Core.Domain.Entities.Interfaces;

using Microsoft.AspNetCore.Mvc;

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
        protected TApplicationService ApplicationService { get; }

        public CoreAPIController(TApplicationService applicationService)
        {
            ApplicationService = applicationService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync()
        {
            var data = await ApplicationService.GetAsync();
            return Ok(new CoreResultModel(HttpStatusCode.OK, data: data));
        }

        [HttpPost]
        public virtual async Task<IActionResult> PostAsync(TViewModel viewModel)
        {
            var data = await ApplicationService.CreateAsync(viewModel);
            return Ok(new CoreResultModel(data != null ? HttpStatusCode.Created : HttpStatusCode.BadRequest, data: data));
        }

        [HttpPut]
        public virtual async Task<IActionResult> PutAsync(TViewModel viewModel)
        {
            var data = await ApplicationService.UpdateAsync(viewModel);
            return Ok(new CoreResultModel(data != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest, data: data));
        }

        [HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(long id)
        {
            var data = await ApplicationService.DeleteAsync(id);
            return Ok(new CoreResultModel(data != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest, data: data));
        }
    }
}
