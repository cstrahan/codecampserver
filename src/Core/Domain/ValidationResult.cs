using System.Collections.Generic;
using System.Linq;

namespace CodeCampServer.Core.Domain
{
	public class ValidationResult
	{
		public bool IsValid { get; private set; }
		public ValidationError[] ErrorMessages { get; private set; }

		public ValidationResult(params ValidationResult[] results)
			: this(results.SelectMany(result => result.ErrorMessages).ToArray())
		{
		}

		public ValidationResult(params ValidationError[] errorMessages)
		{
			ErrorMessages = errorMessages ?? new ValidationError[0];

			if (ErrorMessages.Length == 0)
			{
				IsValid = true;
			}
		}

		public ValidationResult() : this(new ValidationError[0])
		{
		}

		public static implicit operator bool(ValidationResult validationResult)
		{
			return validationResult.IsValid;
		}

		public IDictionary<string, string> GetErrors()
		{
			return ErrorMessages.ToDictionary(e => e.Key, e => e.Message);
		}
	}
}