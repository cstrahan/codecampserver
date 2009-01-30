using System;
using System.Collections;
using System.Collections.Generic;
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
			SelectList selectList = GetSelectList();
			return InputSpecification.Helper.DropDownList(InputSpecification.InputName, selectList,
			                                              InputSpecification.CustomAttributes);
		}

		private SelectList GetSelectList()
		{
			TEntity[] list = GetList();

			SelectList selectList =
				GetSelectListForDropDown(list,
				                         true, GetSelectedValue(), null, GetDisplayPropertyExpression());
			return selectList;
		}

		protected abstract Expression<Func<TEntity, string>> GetDisplayPropertyExpression();

		protected abstract TEntity[] GetList();

		public static SelectList GetSelectListForDropDown<T>(IEnumerable list, bool includeBlankOption, Guid? selectedValue,
		                                                     string displayValueToExclude,
		                                                     Expression<Func<T, string>> displayProperty)
			where T : PersistentObject, new()
		{
			IList<HtmlExtensions.DropDownListItem<Guid>> codes = new List<HtmlExtensions.DropDownListItem<Guid>>();

			HtmlExtensions.DropDownListItem<Guid> selectedItem = null;

			if (includeBlankOption)
			{
				var empty = new HtmlExtensions.DropDownListItem<Guid> {DisplayName = string.Empty, Value = Guid.Empty};
				codes.Add(empty);
			}

			foreach (T enumValue in list)
			{
				string displayValue = ExpressionHelper.Evaluate(displayProperty, enumValue).ToString();

				var listItem = new HtmlExtensions.DropDownListItem<Guid> {DisplayName = displayValue, Value = enumValue.Id};

				if (enumValue.Id == selectedValue)
				{
					selectedItem = listItem;
				}

				if (displayValueToExclude != displayValue)
					codes.Add(listItem);
			}

			return selectedItem != null
			       	? new SelectList(codes, "Value", "DisplayName", selectedItem.Value)
			       	: new SelectList(codes, "Value", "DisplayName");
		}

		private Guid? GetSelectedValue()
		{
			var value = (TEntity) GetValue();

			if (value != null) return value.Id;
			return null;
		}
	}
}