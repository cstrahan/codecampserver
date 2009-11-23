using System.Linq.Expressions;

namespace UITestHelper
{
	public class DisabledField:IInputWrapper
	{
		private readonly Expression _property;

		public DisabledField(Expression property)
		{
			_property = property;
		}
	}
}