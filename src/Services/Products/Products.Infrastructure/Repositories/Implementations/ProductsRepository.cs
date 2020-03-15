using Core.Infrastructure.Repositories.Implementations;

using Products.Domain.Entities.Implementations;
using Products.Domain.Repositories.Interfaces;
using Products.Infrastructure.Persistence.DatabaseContexts.Interfaces;

namespace Products.Infrastructure.Repositories.Implementations
{
    public class ProductsRepository : CoreRepository<Product>, IProductsRepository
    {
        public ProductsRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            var sdf = 123;
        }
    }
}
