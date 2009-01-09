using System;
using Castle.Components.Validator;
using CodeCampServer.UI.Helpers.ViewPage;
using CodeCampServer.UI.Helpers.ViewPage.InputBuilders;
using CodeCampServer.UI.Models.Forms.Attributes;
using CodeCampServer.UI.Views;
using CodeCampServer.UI.Models.AutoMap;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class InputBuilderFactory : IInputBuilderFactory
	{
		private readonly IInputElementAuthority _elementAuthority;

		public InputBuilderFactory(IInputElementAuthority elementAuthority)
		{
			_elementAuthority = elementAuthority;
		}

		public BaseInputCreator CreateInputCreator(InputBuilder inputBuilder)
		{
			if (_elementAuthority.Forbids(inputBuilder.PropertyInfo))
				return new NullInputBuilder(inputBuilder);

			if (inputBuilder.PropertyInfo.HasCustomAttribute<UserDropDownAttribute>())
				return new UserDropDownBuilder(inputBuilder);

			if (inputBuilder.PropertyInfo.HasCustomAttribute<HiddenAttribute>() ||
			    inputBuilder.PropertyInfo.PropertyType == typeof (Guid))
				return new HiddenInputBuilder(inputBuilder);

			if (inputBuilder.PropertyInfo.PropertyType == typeof (bool))
				return new CheckboxInputBuilder(inputBuilder);

			if (inputBuilder.PropertyInfo.PropertyType == typeof (bool?))
				return new YesNoRadioInputBuilder(inputBuilder);



			if ((inputBuilder.PropertyInfo.HasCustomAttribute<ValidateDateAttribute>()) ||
			    (inputBuilder.PropertyInfo.HasCustomAttribute<ValidateDateTimeAttribute>()))
				return new DateInputBuilder(inputBuilder);


			return new TextBoxInputBuilder(inputBuilder);
		}
	}
}