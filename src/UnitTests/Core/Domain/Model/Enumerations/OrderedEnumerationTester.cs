using CodeCampServer.Core.Domain.Model.Enumerations;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Core.Domain.Model.Enumerations
{
	[TestFixture]
	public class OrderedEnumerationTester
	{
		[Test]
		public void Should_be_able_to_create_ordered_enumeration_with_display_order()
		{
			var enumValue = new OrderedEnumerationStub(0, "1", 5);
			enumValue.Value.ShouldEqual(0);
			enumValue.DisplayName.ShouldEqual("1");
			enumValue.DisplayOrder.ShouldEqual(5);
		}

		[Test]
		public void Should_be_able_to_create_ordered_enumeration_without_display_order()
		{
			var enumValue = new OrderedEnumerationStub(0, "1");
			enumValue.Value.ShouldEqual(0);
			enumValue.DisplayName.ShouldEqual("1");
			enumValue.DisplayOrder.ShouldEqual(0);
		}

		public class OrderedEnumerationStub : OrderedEnumeration
		{
			public OrderedEnumerationStub(int value, string displayName)
				: base(value, displayName) {}

			public OrderedEnumerationStub(int value, string displayName, int displayOrder)
				: base(value, displayName, displayOrder) {}
		}
	}
}