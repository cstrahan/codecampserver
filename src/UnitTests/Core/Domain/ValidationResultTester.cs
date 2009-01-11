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
		public void When_empty_array_is_passed_result_should_be_valid()
		{
			var result = new ValidationResult(new ValidationError[0]);
			result.IsValid.ShouldBeTrue();
		}

		[Test]
		public void When_single_string_is_passed_result_should_not_be_valid()
		{
			var result = new ValidationResult(new ValidationError {Key = "foo"});
			result.IsValid.ShouldBeFalse();
			result.ErrorMessages.ShouldContain(new ValidationError { Key = "foo" });
		}

		[Test]
		public void When_multiple_strings_is_passed_result_should_not_be_valid()
		{
			var result = new ValidationResult(new ValidationError { Key = "foo" }, new ValidationError { Key = "bar" });
			result.IsValid.ShouldBeFalse();
			result.ErrorMessages.ShouldContain(new ValidationError { Key = "foo" });
			result.ErrorMessages.ShouldContain(new ValidationError { Key = "bar" });
		}

		[Test]
		public void When_compared_to_bool_operator_overload_should_work()
		{
			var result = new ValidationResult(new ValidationError { Key = "foo" });
			Assert.That(result == false);

			var result1 = new ValidationResult();
			Assert.That(result1 == true);
		}

		[Test]
		public void When_passed_multiple_ValidateResult_objects_Should_aggregate_the_error_messages()
		{
			var result1 = new ValidationResult(new ValidationError { Key = "foo" });
			var result2 = new ValidationResult();
			var result = new ValidationResult(result1, result2);
			result.IsValid.ShouldBeFalse();
		}
	}
}