using AutoMapper;
using StructureMap.Configuration.DSL;

namespace CodeCampServer.DependencyResolution
{
    public class AutoMapperRegistry : Registry
    {
        public AutoMapperRegistry()
        {
            For<IMappingEngine>().TheDefault.Is.ConstructedBy(() => Mapper.Engine);
        }
    }
}