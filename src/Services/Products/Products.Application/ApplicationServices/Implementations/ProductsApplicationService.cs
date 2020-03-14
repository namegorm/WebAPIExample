using AutoMapper;

using Core.Application.ApplicationServices.Implementations;

using Products.Application.ApplicationServices.Interfaces;
using Products.Application.DTOs.Implementations;
using Products.Application.ViewModels.Implementations;
using Products.Domain.Entities.Implementations;
using Products.Domain.Repositories.Interfaces;
using Products.Domain.UnitOfWorks.Interfaces;

namespace Products.Application.ApplicationServices.Implementations
{
    public class ProductsApplicationService : CoreApplicationService<Product, ProductApplicationDTO, ProductViewModel, IUnitOfWork, IProductsRepository>, IProductsApplicationService
    {
        public ProductsApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }
    }
}
