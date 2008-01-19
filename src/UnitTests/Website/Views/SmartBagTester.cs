using System;
using System.Security.Policy;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Website.Views
{
	[TestFixture]
	public class SmartBagTester
	{
		[Test]
		public void ShouldRetrieveSingleObjectByType()
		{
			SmartBag bag = new SmartBag();
			Url url = new Url("/asdf"); //arbitrary object
			bag.Add(url);

			Assert.That(bag.Get<Url>(), Is.EqualTo(url));
			Assert.That(bag.Get(typeof(Url)), Is.EqualTo(url));
		}

		[Test, ExpectedException(ExceptionType = typeof(ArgumentException), ExpectedMessage = "You can only add one default object for type 'System.Security.Policy.Url'.")]
		public void AddingTwoDefaultObjectsOfSameTypeThrows()
		{
			Url url1 = new Url("/1");
			Url url2 = new Url("/2");

			SmartBag bag = new SmartBag();
			bag.Add(url1);
			bag.Add(url2);
		}

		[Test, ExpectedException(typeof(ArgumentException), ExpectedMessage = "No object exists that is of type 'System.Security.Policy.Url'.")]
		public void ShouldGetMeaningfulExceptionIfObjectDoesntExist()
		{
			SmartBag bag = new SmartBag();
			Url url = bag.Get<Url>();
		}

		[Test]
		public void ShouldReportContainsCorrectly()
		{
			SmartBag bag = new SmartBag();
			bag.Add(new Url("/2"));

			Assert.That(bag.Contains<Url>());
			Assert.That(bag.Contains(typeof(Url)));
		}
	}
}