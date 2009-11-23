using System.Linq.Expressions;

namespace UITestHelper
{
	public class VerifyField : IInputWrapper
	{
		private readonly Expression _property;
		private readonly object _value;

		public VerifyField(Expression property,object value)
		{
			_property = property;
			_value = value;
		}
	}
}