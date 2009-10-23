using AutoMapper;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.ObjectMapping.ConfigurationProfiles;

namespace CodeCampServer.Infrastructure.ObjectMapping
{
	public class AutoMapperConfiguration : IRequiresConfigurationOnStartup
	{
		public static void Configure()
		{
			Mapper.Initialize(x =>
			                  	{
			                  		x.ConstructServicesUsing(type => DependencyRegistrar.Resolve(type));
			                  		x.AddProfile<AutoMapperProfile>();
			                  		x.AddProfile<MeetingMapperProfile>();
			                  		x.AddProfile<UserGroupMapperProfile>();
			                  		x.AddProfile<MeetingMessageMapperProfile>();
			                  		x.AddProfile<UserMapperProfile>();
			                  		x.AddProfile<LoginMapperProfile>();
			                  	});
		}

		void IRequiresConfigurationOnStartup.Configure()
		{
			Configure();
		}
	}
}