using CodeCampServer.Core.Domain;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Core.Domain
{
	[TestFixture]
	public class ValidationResultTester
	{
		[Test]
		public void When_nothing_is_passed_result_should_be_valid()
		{
			var result = new ValidationResult();
			result.IsValid.ShouldBeTrue();
		}

		[Test]
		public void When_compared_to_bool_operator_overload_should_work()
		{
			var result = new ValidationResult().AddError<object>(o=>o.GetType(), "foo");
			Assert.That(result == false);

			var result1 = new ValidationResult();
			Assert.That(result1 == true);
		}

		[Test]
		public void When_passed_multiple_errors_Should_aggregate_the_error_messages()
		{
			var result = new ValidationResult();
			result.AddError<object>(o => o.GetType(), "foo");
			result.AddError<object>(o => o.GetType(), "bar");
			result.GetAllErrors().Count.ShouldEqual(1);
		}
	}
}