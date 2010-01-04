using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using CodeCampServer.UI.Helpers.Attributes;

namespace UITestHelper
{
	public class FluentForm<TFormType> : TestBase
	{
		private TFormType _form;
		private readonly IBrowserDriver _browserDriver;
		private readonly LinkedList<InputWrapperBase<TFormType>> _inputWrappers = new LinkedList<InputWrapperBase<TFormType>>();
		private readonly LinkedList<IInputWrapper> _assertWrappers = new LinkedList<IInputWrapper>();

		public FluentForm(IBrowserDriver browserDriver)
		{
			_browserDriver = browserDriver;
			var type = typeof(TFormType);
			var defaultConstructor = type.GetConstructor(new Type[0]);
			var formInstance = (TFormType)defaultConstructor.Invoke(new Object[0]);
			_form = formInstance;
		}

		public LinkedList<InputWrapperBase<TFormType>> InputWrappers
		{
			get {
				return _inputWrappers;
			}
		}

		public FluentForm<TFormType> WithInput(Expression<Func<TFormType, object>> property, string text)
		{
			_inputWrappers.AddLast(new InputWrapperBase<TFormType>(property, text));
			return this;
		}

		public FluentForm<TFormType> WithInput(Expression<Func<TFormType, object>> property, DateTime date)
		{
			_inputWrappers.AddLast(new InputWrapperBase<TFormType>(property, date.ToShortDateString()));
			return this;
		}

		public FluentForm<TFormType> VerifyDisabledFields(params Expression<Func<TFormType, object>>[] fields)
		{
			foreach (var field in fields)
			{
				_assertWrappers.AddLast(new DisabledField(field));
			}
			return this;
		}
		public FluentForm<TFormType> VerifyValue(Expression<Func<TFormType, object>> field, object value)
		{
			_assertWrappers.AddLast(new VerifyField(field,value));
			return this;
		}

		public FluentForm<TFormType> VerifyDisabledRadioButton(params Expression<Func<TFormType, bool?>>[] fields)
		{
			foreach (var field in fields)
			{
				_assertWrappers.AddLast(new DisabledField(field));
			}
			return this;
		}

		public FluentForm<TFormType> ShouldHaveValidationSummary()
		{
			_assertWrappers.AddLast(new FormShouldHaveValidationSummary());

			return this;
		}

		public FluentForm<TFormType> ShouldHaveValidationMessage(string message)
		{
			_assertWrappers.AddLast(new ValidationMessage(message));
			return this;
		}

		public void Submit(string buttonName)
		{
			SetInputs();
			_browserDriver.ClickButton(buttonName);
		}

		private void SetInputs()
		{
			foreach (var inputWrapper in _inputWrappers)
			{
				_browserDriver.SetInput(inputWrapper);
			}
		}

		public void AssertAll()
		{
		}

	}
}