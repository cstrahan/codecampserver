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
			var result = new ValidationResult(new string[0]);
			result.IsValid.ShouldBeTrue();
		}

		[Test]
		public void When_single_string_is_passed_result_should_not_be_valid()
		{
			var result = new ValidationResult("foo");
			result.IsValid.ShouldBeFalse();
			result.ErrorMessages.ShouldContain("foo");
		}

		[Test]
		public void When_multiple_strings_is_passed_result_should_not_be_valid()
		{
			var result = new ValidationResult("foo", "bar");
			result.IsValid.ShouldBeFalse();
			result.ErrorMessages.ShouldContain("foo");
			result.ErrorMessages.ShouldContain("bar");
		}

		[Test]
		public void When_compared_to_bool_operator_overload_should_work()
		{
			var result = new ValidationResult("ls");
			Assert.That(result == false);

			var result1 = new ValidationResult();
			Assert.That(result1 == true);
		}

		[Test]
		public void When_passed_multiple_ValidateResult_objects_Should_aggregate_the_error_messages()
		{
			var result1 = new ValidationResult("foo");
			var result2 = new ValidationResult();
			var result = new ValidationResult(result1, result2);
			result.IsValid.ShouldBeFalse();
		}
	}
}