using AutoMapper;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.Automapper.ObjectMapping;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.UI
{
	[TestFixture]
	public class AutoMapperConfigurationTester
	{
		[Test]
		public void Should_map_dtos()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			AutoMapperConfiguration.Configure();
			Mapper.AssertConfigurationIsValid();
		}
	}
}