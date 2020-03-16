using Core.Domain.Entities.Interfaces;

namespace Products.Domain.Entities.Implementations
{
    public class Product : ICoreDomainEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
