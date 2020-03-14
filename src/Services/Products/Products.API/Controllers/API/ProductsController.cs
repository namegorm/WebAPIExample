using Core.API.Controllers.API;

using Microsoft.AspNetCore.Mvc;

using Products.Application.ApplicationServices.Interfaces;
using Products.Application.DTOs.Implementations;
using Products.Application.ViewModels.Implementations;
using Products.Domain.Entities.Implementations;

namespace Products.API.Controllers.API
{
    [ApiVersion("1.0")]
    [Route("api/v{:apiVersion}/[controller]")]
    public class ProductsController : CoreAPIController<Product, ProductApplicationDTO, ProductViewModel, IProductsApplicationService>
    {
        public ProductsController(IProductsApplicationService applicationService)
            : base(applicationService)
        {
        }
    }
}
