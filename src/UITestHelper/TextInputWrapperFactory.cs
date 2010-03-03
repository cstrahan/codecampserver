using System.Linq.Expressions;
using System.Reflection;

namespace UITestHelper
{
    public class TextInputWrapperFactory : IInputWrapperFactory
    {
        public bool CanHandle(PropertyInfo info)
        {
            return true;
        }

        public IInputWrapper Create(LambdaExpression expression, string text)
        {
            return new TextInputWrapper(expression,text);
        }
    }
}