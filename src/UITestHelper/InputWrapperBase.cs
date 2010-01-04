using System;
using System.Linq.Expressions;

namespace UITestHelper
{
	public class InputWrapperBase<TFormType> : IInputWrapper
	{
		private readonly string _jsEventToFire;
		private readonly Expression<Func<TFormType, object>> _expression;
		private readonly object _value;

		public InputWrapperBase(Expression<Func<TFormType, object>> expression, object value)
		{
			_expression = expression;
			_value = value;
		}

//		public InputWrapperBase(Expression expression, string value, string jsEventToFire):this(expression,value)
//		{
//			_jsEventToFire = jsEventToFire;
//		}

		public string Value
		{
			get { return _value.ToString(); }
		}

		public Expression<Func<TFormType, object>> Property
		{
			get { return _expression; }
		}
	}
}