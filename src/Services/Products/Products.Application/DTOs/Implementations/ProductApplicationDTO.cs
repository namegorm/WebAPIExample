using Core.Application.DTOs.Interfaces;
using Core.Application.Mapping.Interfaces;

using Products.Domain.Entities.Implementations;

namespace Products.Application.DTOs.Implementations
{
    public class ProductApplicationDTO : ICoreApplicationDTO<Product>, ICoreMapFrom<Product>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
