
using Core.API.Controllers.API;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Products.Application.ApplicationServices.Interfaces;
using Products.Application.DTOs.Implementations;
using Products.Application.ViewModels.Implementations;
using Products.Domain.Entities.Implementations;

namespace Products.API.Controllers.API
{
    [ApiVersion("1.0")]
    public class ProductsController : CoreAPIController<Product, ProductApplicationDTO, ProductViewModel, IProductsApplicationService>
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, IProductsApplicationService applicationService)
            : base(logger, applicationService)
        {
            _logger = logger;
        }
    }
}
