using System.Linq.Expressions;
using MvcContrib.UI.InputBuilder.Helpers;

namespace UITestHelper
{
    public abstract class BaseInputWrapper<TInputValue> : IInputWrapper
    {
        protected string _inputName;
        protected TInputValue _value;

        protected BaseInputWrapper(TInputValue value, string name)
        {            
            _value = value;
            _inputName = name;                
        }

        public abstract void AssertInputValueMatches(IBrowserDriver browserDriver);

        public abstract void SetInput(IBrowserDriver browserDriver);
    }
}