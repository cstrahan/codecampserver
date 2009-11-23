using System;
using System.Linq.Expressions;

namespace UITestHelper
{
	public class ValidationMessage : IInputWrapper
	{
		private readonly string _message;

		public ValidationMessage(string message)
		{
			_message = message;
		}
	}
}