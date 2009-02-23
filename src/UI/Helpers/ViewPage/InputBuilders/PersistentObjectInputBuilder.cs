using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CodeCampServer.Core.Common;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public abstract class PersistentObjectInputBuilder<TEntity> : BaseInputBuilder where TEntity : PersistentObject, new()
	{
		public override bool IsSatisfiedBy(IInputSpecification specification)
		{
			return (typeof (TEntity)).IsAssignableFrom(specification.PropertyInfo.PropertyType);
		}

		protected override string CreateInputElementBase()
		{
			IEnumerable<SelectListItem> selectList = GetSelectList();
			InputSpecification.Helper.ViewData[InputSpecification.InputName] =
				selectList.Where(item => item.Selected).Select(item => item.Value).FirstOrDefault();
			string elementMarkup = InputSpecification.Helper.DropDownList(InputSpecification.InputName, selectList,
			                                                              InputSpecification.CustomAttributes);
			return elementMarkup;
		}

		private IEnumerable<SelectListItem> GetSelectList()
		{
			TEntity[] list = GetList();

			IEnumerable<SelectListItem> selectList =
				GetSelectListForDropDown(list,
				                         true, GetSelectedValue(), null, GetDisplayPropertyExpression());
			return selectList;
		}

		protected abstract Expression<Func<TEntity, string>> GetDisplayPropertyExpression();

		protected abstract TEntity[] GetList();

		public static IEnumerable<SelectListItem> GetSelectListForDropDown<T>(IEnumerable list, bool includeBlankOption,
		                                                                      Guid? selectedValue,
		                                                                      string displayValueToExclude,
		                                                                      Expression<Func<T, string>> displayProperty)
			where T : PersistentObject, new()
		{
			var codes = new List<SelectListItem>();

			if (includeBlankOption)
			{
				var empty = new SelectListItem {Text = string.Empty, Value = Guid.Empty.ToString(), Selected = false};
				codes.Add(empty);
			}

			foreach (T enumValue in list)
			{
				string displayValue = ExpressionHelper.Evaluate(displayProperty, enumValue).ToString();

				var listItem = new SelectListItem() {Text = displayValue, Value = enumValue.Id.ToString()};

				if (enumValue.Id == selectedValue)
				{
					listItem.Selected = true;
				}

				if (displayValueToExclude != displayValue)
					codes.Add(listItem);
			}

			return codes;
		}

		private Guid? GetSelectedValue()
		{
			var value = (TEntity) GetValue();

			if (value != null) return value.Id;
			return null;
		}
	}
}