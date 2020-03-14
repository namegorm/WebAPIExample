using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Application.ApplicationServices.Interfaces
{
    public interface ICoreApplicationService<TDomainEntity, TApplicationDTO, TViewModel>
    {
        Task<IEnumerable<TApplicationDTO>> GetAsync(Expression<Func<TDomainEntity, bool>> expression = default);
        Task<TApplicationDTO> CreateAsync(TViewModel viewModel);
        Task<TApplicationDTO> UpdateAsync(TViewModel viewModel);
        Task<TApplicationDTO> DeleteAsync(long id);
    }
}
