using System;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services.Unique;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace CodeCampServer.UnitTests.Core.Services.Unique
{
	[TestFixture]
	public class EntitySpecificationOfGuidTester : TestBase
	{
		[Test]
		public void Should_indicate_empty_id()
		{
			var spec = new EntitySpecificationOfGuid<User>();
			spec.HasExistingId.ShouldBeFalse();

			spec.Id = Guid.Empty;
			spec.HasExistingId.ShouldBeFalse();
		}

		[Test]
		public void Should_indicate_populated_id()
		{
			var spec = new EntitySpecificationOfGuid<User>{Id = Guid.NewGuid()};
			spec.HasExistingId.ShouldBeTrue();
		}

		[Test]
		public void should_have_existing_id()
		{
			var id = Guid.NewGuid();
			var spec = new EntitySpecificationOfGuid<User> { Id = id };
			spec.ExistingId.ShouldEqual(id);
		}
	}
}