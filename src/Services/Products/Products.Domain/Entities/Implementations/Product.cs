using Core.Domain.Entities.Interfaces;

namespace Products.Domain.Entities.Implementations
{
    public class Product : ICoreDomainEntity
    {
        public long Id { get; set; }
    }
}
