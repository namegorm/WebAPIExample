using Core.Application.Mapping.Interfaces;
using Core.Domain.Entities.Interfaces;

namespace Core.Application.DTOs.Interfaces
{
    public interface ICoreApplicationDTO<TDomainEntity> : ICoreMapFrom<TDomainEntity>
        where TDomainEntity : ICoreDomainEntity
    {
    }
}
