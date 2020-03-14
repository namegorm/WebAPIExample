using System;
using System.Linq;
using System.Reflection;

using AutoMapper;

using Core.Application.Mapping.Interfaces;

namespace Core.Application.Mapping.Initialization
{
    public class CoreMappingProfile : Profile
    {
        public CoreMappingProfile()
        {
            Initialize(GetExecutingAssembly());
        }

        protected virtual void Initialize(Assembly assembly)
        {
            var exportedTypes = assembly.GetExportedTypes();

            var mapFromTypes = exportedTypes.Where(x => x.GetInterface(nameof(ICoreMapFrom<object>)) != null);
            var mapToTypes = exportedTypes.Where(x => x.GetInterface(nameof(ICoreMapTo<object>)) != null);

            foreach (var mapFromType in mapFromTypes)
            {
                var instance = Activator.CreateInstance(mapFromType);
                var mapFromMethod = mapFromType.GetMethod(nameof(ICoreMapFrom<object>.MapFrom));
                mapFromMethod?.Invoke(instance, new[] { this });
            }

            foreach (var mapToType in mapToTypes)
            {
                var instance = Activator.CreateInstance(mapToType);
                var mapToMethod = mapToType.GetMethod(nameof(ICoreMapTo<object>.MapTo));
                mapToMethod?.Invoke(instance, new[] { this });
            }
        }

        protected virtual Assembly GetExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
