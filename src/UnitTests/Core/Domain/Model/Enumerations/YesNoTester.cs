using CodeCampServer.Core.Domain.Model.Enumerations;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Core.Domain.Model.Enumerations
{
	[TestFixture]
	public class YesNoTester
	{
		[Test]
		public void Correctly_parses_from_value()
		{
			Assert.That(YesNo.FromValue(false), Is.EqualTo(YesNo.No));
			Assert.That(YesNo.FromValue(true), Is.EqualTo(YesNo.Yes));
		}

		[Test]
		public void Correctly_parses_to_unknown()
		{
			Assert.That(YesNo.FromValue(null), Is.EqualTo(YesNo.Unknown));
		}

		[Test]
		public void correctly_returns_boolean()
		{
			YesNo.Yes.BooleanValue.ShouldEqual(true);
			YesNo.No.BooleanValue.ShouldEqual(false);
			YesNo.Unknown.BooleanValue.ShouldBeNull();
		}

		[Test]
		public void correctly_returns_is_true()
		{
			YesNo.Yes.IsTrue.ShouldBeTrue();
			YesNo.No.IsTrue.ShouldBeFalse();
			YesNo.Unknown.IsTrue.ShouldBeFalse();
		}
	}
}