using System;
using CodeCampServer.Core;
using CodeCampServer.DependencyResolution;
using CodeCampServer.UI.Models;
using CodeCampServer.UI.Models.AutoMap;
using CodeCampServer.UI.Models.Forms;
using NUnit.Framework;
using System.Linq;

namespace CodeCampServer.UnitTests.UI.Automap
{
	[TestFixture]
	public class AutoMapperDtoTester
	{
		[Test]
		public void Should_map_dtos()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();

			AutoMapperConfiguration.Configure();

			AutoMapper
				.GetAllTypeMaps()
				.ForEach(AssertAllMap);
		}

		private static void AssertAllMap(TypeMap typeMap)
		{
			Func<string, bool> exceptions = s => !((s == "ConfirmPassword" || s == "Password") && typeMap.DtoType == typeof(UserForm));

			string[] unmappedPropertyNames = typeMap.GetUnmappedPropertyNames().Where(exceptions).ToArray();
			string failureMessage =
				string.Format(
					"\nThe following {3} properties on {0} are not mapped: \n\t{2}\nAre the types configured in AutoMapper?\nIt's possible that the corresponding property on {1} was renamed.\nIt's also possible that you need to add a custom mapping expression\n",
					typeMap.DtoType.Name, typeMap.ModelType.Name, string.Join("\n\t", unmappedPropertyNames),
					unmappedPropertyNames.Length);
			if (unmappedPropertyNames.Length != 0)
				Assert.Fail(failureMessage);
		}
	}
}