using Castle.Components.Validator;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.DependencyResolution
{
    public class DependencyRegistry : Registry
    {
        protected override void configure()
        {
            string name = GetType().Assembly.GetName().Name;
            name = name.Substring(0, name.LastIndexOf("."));

            Scan(x =>
                     {
                         x.Assembly(name + ".Core");
                         x.Assembly(name + ".Infrastructure");
                         x.Assembly(name + ".UI");
                         x.With<DefaultConventionScanner>();
                     });

            ForRequestedType<ISessionBuilder>().TheDefaultIsConcreteType<HybridSessionBuilder>();
        }
    }

    public class CastleValidatorRegistry : Registry
    {
        protected override void configure()
        {
            ForRequestedType<IValidatorRunner>().TheDefault.Is.ConstructedBy(
                () => new ValidatorRunner(new CachedValidationRegistry()));
        }
    }
}