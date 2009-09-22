using CodeCampServer.Core.Domain.Model.Enumerations;
using CodeCampServer.Core.Domain.Model.Planning;
using NBehave.Spec.NUnit;
using NUnit.Framework;


namespace CodeCampServer.UnitTests.Core.Domain.Model.Enumerations
{
	[TestFixture]
	public class EnumerationHelperTester
	{
		[Test]
		public void Should_convert_from_value()
		{
			Enumeration value = EnumerationHelper.FromValue(typeof (ProposalStatus), ProposalStatus.Accepted.Value);
			value.ShouldEqual(ProposalStatus.Accepted);
		}

		[Test]
		public void Should_convert_from_value_and_get_null()
		{
            Enumeration value = EnumerationHelper.FromValueOrDefault(typeof(ProposalStatus), 99);
			value.ShouldBeNull();
		}

		[Test]
		public void Should_convert_from_display_name_and_get_null()
		{
            Enumeration value = EnumerationHelper.FromDisplayNameOrDefault(typeof(ProposalStatus), "foolksd");
			value.ShouldBeNull();
		}
	}
}