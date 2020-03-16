using Core.Application.Mapping.Interfaces;
using Core.Application.ViewModels.Interfaces;

using Products.Domain.Entities.Implementations;

namespace Products.Application.ViewModels.Implementations
{
    public class ProductViewModel : ICoreViewModel, ICoreMapTo<Product>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
