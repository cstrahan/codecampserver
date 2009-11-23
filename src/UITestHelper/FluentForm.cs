using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UITestHelper
{
	public class FluentForm<TFormType> : TestBase
	{
		private readonly IBrowserDriver _browserDriver;
		//private readonly FormIdentifier _formClass;
		//private readonly IElementsContainer _container;
		private readonly LinkedList<IInputWrapper> _inputWrappers = new LinkedList<IInputWrapper>();
		private readonly LinkedList<IInputWrapper> _assertWrappers = new LinkedList<IInputWrapper>();

		public FluentForm(IBrowserDriver browserDriver)
		{
			_browserDriver = browserDriver;
		}

		public LinkedList<IInputWrapper> InputWrappers
		{
			get {
				return _inputWrappers;
			}
		}


		//public FluentForm<TFormType> WithSelectListByName(Expression<Func<TFormType, object>> property, object value)
		//{
		//    var propertyName = property.GetName();
		//    var stringValue = value == null ? null : value.ToString();

		//    _inputWrappers.AddLast(new SelectListWrapper(propertyName, stringValue));
		//    return this;
		//}

		//public FluentForm<TFormType> WithSelectListEntity<TEntity>(Expression<Func<TFormType, TEntity>> property, TEntity value) where TEntity : PersistentObject
		//{
		//    var propertyName = property.GetName();
		//    var stringValue = value == null ? null : value.Id.ToString();

		//    _inputWrappers.AddLast(new SelectListWrapper(propertyName, stringValue));
		//    return this;
		//}

		//public FluentForm<TFormType> WithSelectListEntity<TEntity>(Expression<Func<TFormType, Guid?>> property, TEntity value) where TEntity : PersistentObject
		//{
		//    var propertyName = property.GetName();
		//    var stringValue = value == null ? null : value.Id.ToString();

		//    _inputWrappers.AddLast(new SelectListWrapper(propertyName, stringValue));
		//    return this;
		//}

		//public FluentForm<TFormType> WithSelectList<TEnum>(Expression<Func<TFormType, TEnum>> property, TEnum value) where TEnum : Enumeration
		//{
		//    var propertyName = property.GetName();
		//    var stringValue = value == null ? null : value.Value.ToString();

		//    _inputWrappers.AddLast(new SelectListWrapper(propertyName, stringValue));
		//    return this;
		//}

		//public FluentForm<TFormType> WithSelectListEmpty(Expression<Func<TFormType, object>> expression)
		//{
		//    WithSelectListByName(expression, null);
		//    return this;
		//}

		//public FluentForm<TFormType> WithCheckBox(Expression<Func<TFormType, bool>> property, bool value)
		//{
		//    var propertyName = property.GetName();
		//    var stringValue = value.ToString();

		//    _inputWrappers.AddLast(new CheckBoxWrapper(propertyName, stringValue));
		//    return this;
		//}

		//public FluentForm<TFormType> WithCheckBoxByName(Expression<Func<TFormType, object>> property, bool value)
		//{
		//    var propertyName = property.GetName();
		//    var stringValue = value.ToString();

		//    _inputWrappers.AddLast(new CheckBoxWrapper(propertyName, stringValue));
		//    return this;
		//}

		//public FluentForm<TFormType> AndPauseForAjax()
		//{
		//    _inputWrappers.Last.Value.PauseForAjax = true;
		//    return this;
		//}

		public FluentForm<TFormType> WithText(Expression<Func<TFormType, object>> property, string text)
		{
			_inputWrappers.AddLast(new InputWrapperBase(property, text));
			return this;
		}

		public FluentForm<TFormType> WithText(Expression<Func<TFormType, object>> property, string text, string jsEventToFire)
		{
			_inputWrappers.AddLast(new InputWrapperBase(property, text, jsEventToFire));
			return this;
		}


		public FluentForm<TFormType> WithTextDate(Expression<Func<TFormType, object>> property, DateTime? date)
		{

			_inputWrappers.AddLast(new InputWrapperBase(property, date.Value.ToShortDateString()));
			return this;
		}

		public FluentForm<TFormType> WithTextDate(Expression<Func<TFormType, object>> property, DateTime? date, string jsEventToFire)
		{
			_inputWrappers.AddLast(new InputWrapperBase(property, date.Value.ToShortDateString(), jsEventToFire));
			return this;
		}


		//public FluentForm<TFormType> WithRadioButton(Expression<Func<TFormType, bool?>> property, bool yesOrNo)
		//{
		//    string stringValue = yesOrNo ? "True" : "False";

		//    var propertyName = property.GetId(stringValue);

		//    _inputWrappers.AddLast(new RadionInputWrapper(propertyName, "True"));
		//    return this;
		//}

		//public FluentForm<TFormType> WithRadioButton(Expression<Func<TFormType, Guid?>> property)
		//{
		//    var propertyName = property.GetId();
		//    _inputWrappers.AddLast(new RadionInputWrapper(propertyName, "True"));
		//    return this;
		//}

		//public FluentForm<TFormType> WithRadioButton<TEnumeration>(Expression<Func<TFormType, TEnumeration>> property, TEnumeration enumeration) where TEnumeration : Enumeration
		//{
		//    string stringValue = enumeration.Value.ToString();

		//    var propertyName = property.GetId(stringValue);

		//    _inputWrappers.AddLast(new RadionInputWrapper(propertyName, "True"));
		//    return this;
		//}

		//public FluentForm<TFormType> WithRadioButton(Expression<Func<TFormType, object>> property)
		//{
		//    return WithRadioButton(property, "True");
		//}

		//public FluentForm<TFormType> WithRadioButton(Expression<Func<TFormType, object>> property, string value)
		//{
		//    var propertyName = property.GetId(value);
		//    _inputWrappers.AddLast(new RadionInputWrapper(propertyName, "True"));
		//    return this;
		//}

		//public FluentForm<TFormType> WithPicker(Expression<Func<TFormType, object>> property, INaturalKeyEntity entity)
		//{
		//    var propertyName = property.GetName();

		//    var text = entity == null ? string.Empty : entity.GetKey();

		//    _inputWrappers.AddLast(new TextInputWrapper(propertyName, text));
		//    return this;
		//}

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