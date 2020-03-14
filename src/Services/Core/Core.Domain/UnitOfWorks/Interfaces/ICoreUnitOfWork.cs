using System.Threading.Tasks;

namespace Core.Domain.UnitOfWorks.Interfaces
{
    public interface ICoreUnitOfWork
    {
        Task<int> CommitAsync();
        TRepository GetRepository<TRepository>() where TRepository : class;
    }
}
