using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Services.Unique;
using CodeCampServer.UnitTests.Core.Domain;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Core.Services.Unique
{
	[TestFixture]
	public class UniquenessCheckerTester : TestBase
	{
		[Test]
		public void should_indicate_success_when_input_has_a_unique_property()
		{
			var specification = new EntitySpecificationOfGuid<TestModel>();

			var counter = EntityCounterSpy<TestModel>.With().StubbedCount(0);

			var checker = new UniquenessChecker(counter);
			var result = checker.IsUnique(specification);

			result.ShouldBeTrue();

			counter.Specification.ShouldBeTheSameAs(specification);
		}

		[Test]
		public void should_indicate_failure_when_input_property_is_not_unique()
		{
			var specification = new EntitySpecificationOfGuid<TestModel>();

			var counter = EntityCounterSpy<TestModel>.With().StubbedCount(1);

			var checker = new UniquenessChecker(counter);
			var result = checker.IsUnique(specification);

			result.ShouldBeFalse();

			counter.Specification.ShouldBeTheSameAs(specification);
		}

		[Test]
		public void should_build_failure_message()
		{
			Expression<Func<TestModel, object>> propertyExpression = x => x.MyInt;
			var propertyValue = 1;
			var input = new TestModel {MyInt = propertyValue};

			var counter = EntityCounterSpy<TestModel>.With().StubbedCount(1);

			var rule = new UniquenessChecker(null);
			var result = rule.BuildFailureMessage(propertyValue, propertyExpression);

			result.ShouldEqual("Property 'MyInt' should be unique, but the value '1' already exists.");
		}

		private class TestModel : AuditedPersistentObjectOfGuid
		{
			public int MyInt { get; set; }

			protected override object GetEmptyId()
			{
				return Guid.Empty;
			}
		}
	}
}