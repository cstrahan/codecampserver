using System.Linq;

namespace CodeCampServer.Core.Domain
{
	public class ValidationResult
	{
		public bool IsValid { get; private set; }
		public string[] ErrorMessages { get; private set; }

		public ValidationResult(params ValidationResult[] results)
			: this(results.SelectMany(result => result.ErrorMessages).ToArray())
		{
		}

		public ValidationResult(params string[] errorMessages)
		{
			ErrorMessages = errorMessages ?? new string[0];

			if (ErrorMessages.Length == 0)
			{
				IsValid = true;
			}
		}

		public ValidationResult() : this(new string[0])
		{
		}

		public static implicit operator bool(ValidationResult validationResult)
		{
			return validationResult.IsValid;
		}
	}
}