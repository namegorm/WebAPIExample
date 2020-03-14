using Core.Application.ApplicationServices.Interfaces;

using Products.Application.DTOs.Implementations;
using Products.Application.ViewModels.Implementations;
using Products.Domain.Entities.Implementations;

namespace Products.Application.ApplicationServices.Interfaces
{
    public interface IProductsApplicationService : ICoreApplicationService<Product, ProductApplicationDTO, ProductViewModel>
    {
    }
}
