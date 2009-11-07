using System.Collections.Generic;
using CodeCampServer.Core.Common;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Core.Domain.Model
{
	[TestFixture]
	public class ObjectExtensionsTester
	{
		[Test]
		public void Should_get_property_value_by_name()
		{
			var something = new {Value = 123};
			something.GetPropertyValue("Value").ShouldEqual(123);
		}

		[Test]
		public void Should_get_dictionary_of_property_values()
		{
			var something = new {Value = 123, Another = "Test"};
			IDictionary<string, object> dictionary = something.ToDictionary();
			dictionary["Value"].ShouldEqual(123);
			dictionary["Another"].ShouldEqual("Test");
		}
	}
}