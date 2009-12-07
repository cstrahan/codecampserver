using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CodeCampServer.Core.Common;

namespace CodeCampServer.Core.Domain
{
	public class ValidationResult
	{
		public bool IsValid { get; private set; }
		private readonly IDictionary<string, string[]> _errors = new Dictionary<string, string[]>();

		public ValidationResult()
		{
			IsValid = true;
		}

		public ValidationResult AddError<T>(Expression<Func<T, object>> messageExpression, string message)
		{
			string key = UINameHelper.BuildNameFrom(messageExpression);

			if (_errors.ContainsKey(key))
			{
				var strings = new List<string>(_errors[key]) {message};
				_errors[key] = strings.ToArray();
			}
			else
			{
				_errors.Add(key, new[] {message});
			}

			IsValid = false;

			return this;
		}

		public string[] GetErrors(string key)
		{
			return _errors[key];
		}

		public string[] GetErrors<T>(Expression<Func<T, object>> messageExpression)
		{
			return GetErrors(UINameHelper.BuildNameFrom(messageExpression));
		}

		public IDictionary<string, string[]> GetAllErrors()
		{
			return new Dictionary<string, string[]>(_errors);
		}

		public static implicit operator bool(ValidationResult validationResult)
		{
			return validationResult.IsValid;
		}
	}
}