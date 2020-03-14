using AutoMapper;

namespace Core.Application.Mapping.Interfaces
{
    public interface ICoreMapFrom<T>
    {
        void MapFrom(Profile profile) => profile.CreateMap(typeof(T), this.GetType());
    }
}
