using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Castle.Components.Validator;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Forms;
using MvcContrib.UI.InputBuilder;

namespace CodeCampServer.UI
{
	public class InputBuilderConventions : IModelPropertyConventions
	{
		private readonly IModelPropertyConventions _default = new DefaultConventions();

		public string ExampleForPropertyConvention(PropertyInfo propertyInfo)
		{
			return _default.ExampleForPropertyConvention(propertyInfo);
		}

		public object ValueFromModelPropertyConvention(PropertyInfo propertyInfo, object model)
		{
			if (typeof (IEnumerable<UserSelector>).IsAssignableFrom(propertyInfo.PropertyType))
			{
				var value = propertyInfo.GetValue(model, null) as IEnumerable<UserSelector>;
				var items = new List<SelectListItem>();
				var repo = DependencyRegistrar.Resolve<IUserRepository>();
				foreach (User user in repo.GetAll())
				{
					bool isChecked = value != null && (value).Where(form => form.Id == user.Id).Count() > 0;
					items.Add(new SelectListItem {Selected = isChecked, Text = user.Name, Value = user.Id.ToString()});
				}
				return items;
			}

			return _default.ValueFromModelPropertyConvention(propertyInfo, model);
		}

		public string LabelForPropertyConvention(PropertyInfo propertyInfo)
		{
			if (propertyInfo.AttributeExists<LabelAttribute>())
				return propertyInfo.GetAttribute<LabelAttribute>().Value;

			if (propertyInfo.AttributeExists<RequiredAttribute>())
				return propertyInfo.GetAttribute<RequiredAttribute>().Label;

			return _default.LabelForPropertyConvention(propertyInfo);
		}

		public bool ModelIsInvalidConvention<T>(PropertyInfo propertyInfo, HtmlHelper<T> htmlHelper) where T : class
		{
			return _default.ModelIsInvalidConvention(propertyInfo, htmlHelper);
		}

		public string PropertyNameConvention(PropertyInfo propertyInfo)
		{
			return _default.PropertyNameConvention(propertyInfo);
		}

		public Type PropertyTypeConvention(PropertyInfo propertyInfo)
		{
			return _default.PropertyTypeConvention(propertyInfo);
		}

		public string PartialNameConvention(PropertyInfo propertyInfo)
		{
			if (typeof (IEnumerable<UserSelector>).IsAssignableFrom(propertyInfo.PropertyType))
				return "UserPicker";
			if (propertyInfo.Name.ToLower().Contains("password"))
				return "Password";
			if (typeof (DateTime).IsAssignableFrom(propertyInfo.PropertyType))
				return "DatePicker";
			if (propertyInfo.AttributeExists<MultilineAttribute>())
				return "MultilineText";

			return _default.PartialNameConvention(propertyInfo);
		}


		public PropertyViewModel ModelPropertyBuilder(PropertyInfo propertyInfo, object model)
		{
			if (typeof (IEnumerable<UserSelector>).IsAssignableFrom(propertyInfo.PropertyType))
				return new PropertyViewModel<IEnumerable<SelectListItem>> { Value = (IEnumerable<SelectListItem>)model };
			return _default.ModelPropertyBuilder(propertyInfo, model);
		}

		public bool PropertyIsRequiredConvention(PropertyInfo propertyInfo)
		{
			if (propertyInfo.AttributeExists<ShowAsRequiredAttribute>())
				return true;

			if (propertyInfo.AttributeExists<ValidateNonEmptyAttribute>())
				return true;

			if (propertyInfo.AttributeExists<RequiredDateTimeAttribute>())
				return true;

			return _default.PropertyIsRequiredConvention(propertyInfo);
		}

		public string Layout(string partialName)
		{
			return _default.Layout(partialName);
		}

		public string PartialNameForTypeConvention(Type type)
		{
			return _default.PartialNameForTypeConvention(type);
		}

		public string LabelForTypeConvention(Type type)
		{
			return _default.LabelForTypeConvention(type);
		}
	}
}