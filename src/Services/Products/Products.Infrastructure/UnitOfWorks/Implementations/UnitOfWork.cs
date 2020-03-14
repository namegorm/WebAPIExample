using System;
using System.Collections.Generic;

using Core.Infrastructure.UnitOfWorks.Implementations;

using Products.Domain.Repositories.Interfaces;
using Products.Domain.UnitOfWorks.Interfaces;
using Products.Infrastructure.Persistence.DatabaseContexts.Interfaces;

namespace Products.Infrastructure.UnitOfWorks.Implementations
{
    public class UnitOfWork : CoreUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(IDatabaseContext databaseContext, IProductsRepository productsRepository)
            : base(databaseContext)
        {
            Repositories = new Dictionary<Type, object>
            {
                [typeof(IProductsRepository)] = productsRepository
            };
        }
    }
}
