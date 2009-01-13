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
			SelectList selectList = GetSelectListForDropDown<T>(Enumeration.GetAll<T>(), includeBlankOption, selectedValue, null);

			string html = helper.DropDownList("", listName, selectList);
			return html;
		}

		public static string EnumerationDropDownListWithExclude<T>(this HtmlHelper helper, string listName,
		                                                           bool includeBlankOption, string displayValueToExclude,
		                                                           int? selectedValue) where T : Enumeration, new()
		{
			SelectList selectList = GetSelectListForDropDown<T>(Enumeration.GetAll<T>(), includeBlankOption, selectedValue,
			                                                    displayValueToExclude);

			string html = helper.DropDownList("", listName, selectList);
			return html;
		}

		public static SelectList GetSelectListForDropDown<T>(IEnumerable<T> list, bool includeBlankOption, int? selectedValue,
		                                                     string displayValueToExclude, T first)
			where T : Enumeration, new()
		{
			IEnumerable<T> listWithoutFirst = list.Except(new[] {first});
			var innerList = new List<T>(listWithoutFirst);
			innerList.Insert(0, first);

			return GetSelectListForDropDown<T>(innerList, includeBlankOption, selectedValue, displayValueToExclude);
		}

		public static SelectList GetSelectListForDropDown<T>(IEnumerable list, bool includeBlankOption, int? selectedValue,
		                                                     string displayValueToExclude) where T : Enumeration, new()
		{
			IList<DropDownListItem<int>> codes = new List<DropDownListItem<int>>();

			DropDownListItem<int> selectedItem = null;

			if (includeBlankOption)
			{
				var empty = new DropDownListItem<int> { DisplayName = string.Empty, Value = -1 };
				codes.Add(empty);
			}

			foreach (T enumValue in list)
			{
				var listItem = new DropDownListItem<int> { DisplayName = enumValue.DisplayName, Value = enumValue.Value };

				if (enumValue.Value == selectedValue)
				{
					selectedItem = listItem;
				}

				if (displayValueToExclude != enumValue.DisplayName)
					codes.Add(listItem);
			}

			return selectedItem != null
			       	? new SelectList(codes, "Value", "DisplayName", selectedItem.Value)
			       	: new SelectList(codes, "Value", "DisplayName");
		}

		public static SelectList GetSelectListForDropDown(IEnumerable<Enumeration> list, bool includeBlankOption,
		                                                  int? selectedValue, string displayValueToExclude)
		{
			IList<DropDownListItem<int>> codes = new List<DropDownListItem<int>>();

			DropDownListItem<int> selectedItem = null;

			if (includeBlankOption)
			{
				var empty = new DropDownListItem<int> { DisplayName = string.Empty, Value = -1 };
				codes.Add(empty);
			}

			foreach (Enumeration enumValue in list)
			{
				var listItem = new DropDownListItem<int> { DisplayName = enumValue.DisplayName, Value = enumValue.Value };

				if (enumValue.Value == selectedValue)
				{
					selectedItem = listItem;
				}

				if (displayValueToExclude != enumValue.DisplayName)
					codes.Add(listItem);
			}

			return selectedItem != null
			       	? new SelectList(codes, "Value", "DisplayName", selectedItem.Value)
			       	: new SelectList(codes, "Value", "DisplayName");
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