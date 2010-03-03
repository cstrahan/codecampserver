using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MvcContrib.UI.InputBuilder.Helpers;

namespace UITestHelper
{
    public class InputForm<TFormType>
    {
        private readonly IBrowserDriver _browserDriver;
        protected readonly LinkedList<IInputWrapper> _inputWrappers = new LinkedList<IInputWrapper>();

        public InputForm(IBrowserDriver browserDriver)
        {
            _browserDriver = browserDriver;
            SubmitName = "Submit";
        }

        public string SubmitName { get; set; }

        private LinkedList<IInputWrapper> InputWrappers
        {
            get { return _inputWrappers; }
        }

        public InputForm<TFormType> Input(Expression<Func<TFormType, object>> expression, string text)
        {
            PropertyInfo propertyInfo = ReflectionHelper.FindPropertyFromExpression(expression);

            IInputWrapperFactory factory = GetInputForProperty(propertyInfo);

            return Input(factory.Create(expression, text));
        }

        private IInputWrapperFactory GetInputForProperty(PropertyInfo propertyInfo)
        {            
            return InputWrapperFactory.Factory().Where(factory => factory.CanHandle(propertyInfo)).First();
        }

        public InputForm<TFormType> Input(IInputWrapper wrapper)
        {
            _inputWrappers.AddLast(wrapper);
            return this;
        }

        public IBrowserDriver Submit()
        {
            SetInputs();

            _browserDriver.ClickButton(SubmitName);
            return _browserDriver;
        }

        private void SetInputs()
        {
            EnumerableExtensions.ForEach(_inputWrappers, x => x.SetInput(_browserDriver));
        }
    }
}