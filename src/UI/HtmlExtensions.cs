using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MvcContrib;
using Tarantino.Core.Commons.Model.Enumerations;

namespace CodeCampServer.UI
{
	public static class HtmlExtensions
	{
		public static string EnumerationDropDownList<T>(this HtmlHelper helper, string listName, bool includeBlankOption)
			where T : Enumeration, new()
		{
			return EnumerationDropDownList<T>(helper, listName, includeBlankOption, null);
		}

		public static string EnumerationDropDownList<T>(this HtmlHelper helper, string listName, bool includeBlankOption,
		                                                int? selectedValue) where T : Enumeration, new()
		{
			return EnumerationDropDownList<T>(helper, listName, includeBlankOption, selectedValue, null);
		}

		public static string EnumerationDropDownList<T>(this HtmlHelper helper, string listName, bool includeBlankOption,
		                                                string selectedName, object htmlOptions) where T : Enumeration, new()
		{
			int? selectedValue = selectedName == null ? (int?) null : Enumeration.FromDisplayName<T>(selectedName).Value;
			return EnumerationDropDownList<T>(helper, listName, includeBlankOption, selectedValue, htmlOptions);
		}

		public static string EnumerationDropDownList<T>(this HtmlHelper helper, string listName, bool includeBlankOption,
		                                                int? selectedValue, object htmlOptions) where T : Enumeration, new()
		{
			IEnumerable<SelectListItem> selectList = GetSelectListForDropDown<T>(Enumeration.GetAll<T>(), includeBlankOption, selectedValue, null);

			string html = helper.DropDownList(listName, selectList);
			return html;
		}

		public static string EnumerationDropDownListWithExclude<T>(this HtmlHelper helper, string listName,
		                                                           bool includeBlankOption, string displayValueToExclude,
		                                                           int? selectedValue) where T : Enumeration, new()
		{
			IEnumerable<SelectListItem> selectList = GetSelectListForDropDown<T>(Enumeration.GetAll<T>(), includeBlankOption, selectedValue,
			                                                    displayValueToExclude);

			string html = helper.DropDownList( listName, selectList);
			return html;
		}

		public static IEnumerable<SelectListItem> GetSelectListForDropDown<T>(IEnumerable<T> list, bool includeBlankOption, int? selectedValue,
		                                                     string displayValueToExclude, T first)
			where T : Enumeration, new()
		{
			IEnumerable<T> listWithoutFirst = list.Except(new[] {first});
			var innerList = new List<T>(listWithoutFirst);
			innerList.Insert(0, first);

			return GetSelectListForDropDown<T>(innerList, includeBlankOption, selectedValue, displayValueToExclude);
		}

		public static IEnumerable<SelectListItem> GetSelectListForDropDown<T>(IEnumerable list, bool includeBlankOption, int? selectedValue,
		                                                     string displayValueToExclude) where T : Enumeration, new()
		{
			var codes = new List<SelectListItem>();

			if (includeBlankOption)
			{
				var empty = new SelectListItem() { Text = string.Empty, Value = (-1).ToString() };
				codes.Add(empty);
			}

			foreach (T enumValue in list)
			{
				var listItem = new SelectListItem() { Text = enumValue.DisplayName, Value = enumValue.Value.ToString() };

				if (enumValue.Value == selectedValue)
				{
					listItem.Selected = true;
				}

				if (displayValueToExclude != enumValue.DisplayName)
					codes.Add(listItem);
			}

			return codes;
		}

		public static IEnumerable<SelectListItem> GetSelectListForDropDown(IEnumerable<Enumeration> list, bool includeBlankOption,
		                                                  int? selectedValue, string displayValueToExclude)
		{
			var codes = new List<SelectListItem>();

			if (includeBlankOption)
			{
				var empty = new SelectListItem() { Text = string.Empty, Value = "-1" };
				codes.Add(empty);
			}

			foreach (Enumeration enumValue in list)
			{
				var listItem = new SelectListItem() { Text = enumValue.DisplayName, Value = enumValue.Value.ToString() };

				if (enumValue.Value == selectedValue)
				{
					listItem.Selected = true;
				}

				if (displayValueToExclude != enumValue.DisplayName)
					codes.Add(listItem);
			}

			return codes;
		}


		public static string AccessibleCheckBox(this HtmlHelper helper, string checkboxName, string checkboxLabel,
		                                        bool isChecked)
		{
			string isCheckedHtml = isChecked ? "checked=\"checked\"" : string.Empty;
			string inputHtml = string.Format("<input type=\"checkbox\" value=\"true\" {0} name=\"{1}\" id=\"{1}\"/>",
			                                 isCheckedHtml, checkboxName);
			string labelHtml = string.Format("<label for=\"{0}\">{1}</label>", checkboxName, checkboxLabel);
			return inputHtml + labelHtml;
		}

		public class DropDownListItem<T>
		{
			public T Value { get; set; }
			public string DisplayName { get; set; }
		}

		public static string PickLink(this HtmlHelper helper, Guid id, Type entityType)
		{
			return
				string.Format(
					"<a rel=\"entityPicker\" title=\"{0}:{1}\" href=\"#\"><img src=\"/images/buttons/star.png\" alt=\"{0}:{1}\" /></a>",
					entityType.Name, id);
		}

		public static string StandardHeading(this HtmlHelper helper, string headerText)
		{
			if (string.IsNullOrEmpty(headerText))
			{
				headerText = @"&nbsp;";
			}
			string html = string.Format(@"<p class=""dataHeading"">{0}</p>", headerText);
			return html;
		}

		public static void RenderSubController(this HtmlHelper helper, string subcontroller)
		{
			helper.ViewContext.ViewData.Get<Action>(subcontroller).Invoke();
		}

		public static string ActionLink<TController>(this HtmlHelper helper, string linkText,
		                                             Expression<Func<TController, object>> actionExpression)
		{
			string controllerName = typeof (TController).GetControllerName();
			string actionName = actionExpression.GetActionName();

			return helper.ActionLink(linkText, actionName, controllerName);
		}
	}
}