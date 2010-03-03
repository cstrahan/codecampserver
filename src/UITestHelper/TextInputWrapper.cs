using System.Linq.Expressions;
using MvcContrib.UI.InputBuilder.Helpers;

namespace UITestHelper
{
    public class TextInputWrapper : BaseInputWrapper<string>
    {
        public TextInputWrapper(LambdaExpression property, string value) : base(value, ReflectionHelper.BuildNameFrom(property)) {}

        public override void AssertInputValueMatches(IBrowserDriver browserDriver)
        {
            string actualValue = browserDriver.GetValue(_inputName);

            UITestExceptionFactory.AssertEquals(_value, actualValue, "Asserting value for input '" + _inputName + "'.");
        }

        public override void SetInput(IBrowserDriver browserDriver)
        {
            browserDriver.SetValue(_inputName, _value);
        }
    }
}