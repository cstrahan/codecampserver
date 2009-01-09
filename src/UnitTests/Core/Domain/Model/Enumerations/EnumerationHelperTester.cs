using CodeCampServer.Core.Domain.Model.Enumerations;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Tarantino.Core.Commons.Model.Enumerations;
using Tarantino.Core.Deployer.Model;

namespace CodeCampServer.UnitTests.Core.Domain.Model.Enumerations
{
	[TestFixture]
	public class EnumerationHelperTester
	{
		[Test]
		public void Should_convert_from_value()
		{
			Enumeration value = EnumerationHelper.FromValue(typeof (DeploymentResult), DeploymentResult.Success.Value);
			value.ShouldEqual(DeploymentResult.Success);
		}

		[Test]
		public void Should_convert_from_value_and_get_null()
		{
			Enumeration value = EnumerationHelper.FromValueOrDefault(typeof (DeploymentResult), 99);
			value.ShouldBeNull();
		}

		[Test]
		public void Should_convert_from_display_name_and_get_null()
		{
			Enumeration value = EnumerationHelper.FromDisplayNameOrDefault(typeof (DeploymentResult), "foolksd");
			value.ShouldBeNull();
		}
	}
}