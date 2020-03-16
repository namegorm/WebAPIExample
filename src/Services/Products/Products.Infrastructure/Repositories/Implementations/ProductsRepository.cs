using Core.Infrastructure.Repositories.Implementations;

using Microsoft.Extensions.Logging;

using Products.Domain.Entities.Implementations;
using Products.Domain.Repositories.Interfaces;
using Products.Infrastructure.Persistence.DatabaseContexts.Interfaces;

namespace Products.Infrastructure.Repositories.Implementations
{
    public class ProductsRepository : CoreRepository<Product>, IProductsRepository
    {
        private readonly ILogger<ProductsRepository> _logger;

        public ProductsRepository(ILogger<ProductsRepository> logger, IDatabaseContext databaseContext)
            : base(logger, databaseContext)
        {
            _logger = logger;
        }
    }
}
