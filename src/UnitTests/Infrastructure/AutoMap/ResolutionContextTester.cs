using CodeCampServer.Infrastructure.AutoMap;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Infrastructure.AutoMap
{
	[TestFixture]
	public class ResolutionContextTester
	{
		public string DummyProp { get; set; }

		[Test]
		public void When_creating_a_new_context_from_an_existing_context_Should_preserve_context_type_map()
		{
			var map = new TypeMap(typeof (int), typeof (string));

			var context = new ResolutionContext(map, 5, typeof (int), typeof (string));

			ResolutionContext newContext = context.CreateValueContext(10);

			newContext.ContextTypeMap.ShouldNotBeNull();
		}
	}
}