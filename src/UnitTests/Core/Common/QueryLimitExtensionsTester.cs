using CodeCampServer.Core.Common;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Core.Common
{
	[TestFixture]
	public class QueryLimitExtensionsTester : TestBase
	{
		[Test]
		public void should_have_good_message_for_query_limits()
		{
			var atLimit = new string[QueryLimitExtensions.ResultsLimit];
			var smaller = new string[QueryLimitExtensions.ResultsLimit - 1];
			var bigger = new string[QueryLimitExtensions.ResultsLimit + 1];

			atLimit.GetSizeMessage().ShouldEqual(string.Format("{0} rows (limited)", QueryLimitExtensions.ResultsLimit));
			smaller.GetSizeMessage().ShouldEqual(string.Format("{0} rows", QueryLimitExtensions.ResultsLimit - 1));
			bigger.GetSizeMessage().ShouldEqual(string.Format("{0} rows", QueryLimitExtensions.ResultsLimit + 1));
		}
	}
}