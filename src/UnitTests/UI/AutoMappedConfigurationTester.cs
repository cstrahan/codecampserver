using CodeCampServer.Core;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.AutoMap;
using CodeCampServer.UI;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.UI
{
	[TestFixture]
	public class AutoMapperConfigurationTester
	{
		[Test]
		public void Should_map_dtos()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();

			AutoMapperConfiguration.Configure();

			TypeMap[] maps = AutoMapper.GetAllTypeMaps();
			maps.ForEach(AssertAllMap);
		}

		private static void AssertAllMap(TypeMap typeMap)
		{
			string[] unmappedPropertyNames = typeMap.GetUnmappedPropertyNames();
			string failureMessage =
				string.Format(
					"\nThe following {3} properties on {0} are not mapped: \n\t{2}\nAre the types configured in AutoMapper?\nIt's possible that the corresponding property on {1} was renamed.\nIt's also possible that you need to add a custom mapping expression\n",
					typeMap.DestinationType.Name, typeMap.SourceType.Name, string.Join("\n\t", unmappedPropertyNames),
					unmappedPropertyNames.Length);

			if (unmappedPropertyNames.Length != 0)
				Assert.Fail(failureMessage);
		}
	}
}