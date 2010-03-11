using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Services;
using CodeCampServer.UnitTests.Core.Domain;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Infrastructure.CommandProcessor.Rules.Unique
{
	[TestFixture]
	public class UniquenessCheckerTester : TestBase
	{
		[Test]
		public void should_indicate_success_when_input_has_a_unique_property()
		{
			Expression<Func<TestModel, object>> propertyExpression = x => x.MyInt;
			var propertyValue = 123;
			var input = new TestModel {MyInt = propertyValue};

			var counter = EntityCounterSpy.With().StubbedCount(0);

			var checker = new UniquenessChecker(counter);
			var result = checker.IsUnique(propertyValue, propertyExpression);

			result.ShouldBeTrue();

			counter.Value.ShouldEqual(propertyValue);
			counter.PropertyName.ShouldEqual("MyInt");
		}

		[Test]
		public void should_indicate_failure_when_input_property_is_not_unique()
		{
			Expression<Func<TestModel, object>> propertyExpression = x => x.MyInt;
			var propertyValue = 2134;
			var input = new TestModel {MyInt = propertyValue};

			var counter = EntityCounterSpy.With().StubbedCount(1);

			var checker = new UniquenessChecker(counter);
			var result = checker.IsUnique(propertyValue, propertyExpression);

			result.ShouldBeFalse();
		}

		[Test]
		public void should_build_failure_message()
		{
			Expression<Func<TestModel, object>> propertyExpression = x => x.MyInt;
			var propertyValue = 1;
			var input = new TestModel {MyInt = propertyValue};

			var counter = EntityCounterSpy.With().StubbedCount(1);

			var rule = new UniquenessChecker(null);
			var result = rule.BuildFailureMessage(propertyValue, propertyExpression);

			result.ShouldEqual("Property 'MyInt' should be unique, but the value '1' already exists.");
		}

		private class TestModel : PersistentObject
		{
			public int MyInt { get; set; }

			protected override object GetEmptyId()
			{
				return Guid.Empty;
			}
		}
	}
}