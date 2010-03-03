using System.Linq.Expressions;
using System.Reflection;

namespace UITestHelper
{
    public interface IInputWrapperFactory
    {
        bool CanHandle(PropertyInfo info);
        IInputWrapper Create(LambdaExpression expression, string text);
    }
}