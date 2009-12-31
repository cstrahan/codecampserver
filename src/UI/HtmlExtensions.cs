using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Common;
using CodeCampServer.UI.Models.Input;
using MvcContrib;
using MvcContrib.UI.Grid;
using MvcContrib.UI.Grid.Syntax;

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
		                                                string selectedName, object htmlOptions)
			where T : Enumeration, new()
		{
			int? selectedValue = selectedName == null ? (int?) null : Enumeration.FromDisplayName<T>(selectedName).Value;
			return EnumerationDropDownList<T>(helper, listName, includeBlankOption, selectedValue, htmlOptions);
		}

		public static string EnumerationDropDownList<T>(this HtmlHelper helper, string listName, bool includeBlankOption,
		                                                int? selectedValue, object htmlOptions)
			where T : Enumeration, new()
		{
			IEnumerable<SelectListItem> selectList = GetSelectListForDropDown<T>(Enumeration.GetAll<T>(),
			                                                                     includeBlankOption, selectedValue, null);

			string html = helper.DropDownList(listName, selectList).ToString();
			return html;
		}

		public static string EnumerationDropDownListWithExclude<T>(this HtmlHelper helper, string listName,
		                                                           bool includeBlankOption, string displayValueToExclude,
		                                                           int? selectedValue) where T : Enumeration, new()
		{
			IEnumerable<SelectListItem> selectList = GetSelectListForDropDown<T>(Enumeration.GetAll<T>(),
			                                                                     includeBlankOption, selectedValue,
			                                                                     displayValueToExclude);

			string html = helper.DropDownList(listName, selectList).ToString();
			return html;
		}

		public static IEnumerable<SelectListItem> GetSelectListForDropDown<T>(IEnumerable<T> list,
		                                                                      bool includeBlankOption,
		                                                                      int? selectedValue,
		                                                                      string displayValueToExclude, T first)
			where T : Enumeration, new()
		{
			IEnumerable<T> listWithoutFirst = list.Except(new[] {first});
			var innerList = new List<T>(listWithoutFirst);
			innerList.Insert(0, first);

			return GetSelectListForDropDown<T>(innerList, includeBlankOption, selectedValue, displayValueToExclude);
		}

		public static IEnumerable<SelectListItem> GetSelectListForDropDown<T>(IEnumerable list, bool includeBlankOption,
		                                                                      int? selectedValue,
		                                                                      string displayValueToExclude)
			where T : Enumeration, new()
		{
			var codes = new List<SelectListItem>();

			if (includeBlankOption)
			{
				var empty = new SelectListItem {Text = string.Empty, Value = (-1).ToString()};
				codes.Add(empty);
			}

			foreach (T enumValue in list)
			{
				var listItem = new SelectListItem {Text = enumValue.DisplayName, Value = enumValue.Value.ToString()};

				if (enumValue.Value == selectedValue)
				{
					listItem.Selected = true;
				}

				if (displayValueToExclude != enumValue.DisplayName)
					codes.Add(listItem);
			}

			return codes;
		}

		public static IEnumerable<SelectListItem> GetSelectListForDropDown(IEnumerable<Enumeration> list,
		                                                                   bool includeBlankOption,
		                                                                   int? selectedValue,
		                                                                   string displayValueToExclude)
		{
			var codes = new List<SelectListItem>();

			if (includeBlankOption)
			{
				var empty = new SelectListItem {Text = string.Empty, Value = "-1"};
				codes.Add(empty);
			}

			foreach (Enumeration enumValue in list)
			{
				var listItem = new SelectListItem {Text = enumValue.DisplayName, Value = enumValue.Value.ToString()};

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

			return helper.ActionLink(linkText, actionName, controllerName, null,
			                         new {@class = "action-link " + linkText.ToLower()}).ToString();
		}

		public static IGridColumn<T> PartialCell<T>(this IGridColumn<T> column, string partialName) where T : class
		{
			column.CustomItemRenderer = (context, item) =>
			                            	{
			                            		IView view = context.ViewEngines.TryLocatePartial(context.ViewContext, partialName);
			                            		var newViewData = new ViewDataDictionary<T>(item);
			                            		var newContext = new ViewContext(context.ViewContext, context.ViewContext.View,
			                            		                                 newViewData, context.ViewContext.TempData, context.Writer);
			                            		context.Writer.Write("<td>");
			                            		view.Render(newContext, context.Writer);
			                            		context.Writer.Write("</td>");
			                            	};
			return column;
		}

		public static IGridWithOptions<T> WithID<T>(this IGridWithOptions<T> grid, string htmlID) where T : class
		{
			return grid.Attributes(id => htmlID);
		}

		public static IGridWithOptions<T> WithClass<T>(this IGridWithOptions<T> grid, string htmlID) where T : class
		{
			return grid.Attributes(@class => htmlID);
		}

		public static IGridColumn<T> PartialInCell<T>(this IGridColumn<T> column, string viewName) where T : class
		{
			return column;
		}

		public static IGridWithOptions<T> AutoColumns<T>(this IGridWithOptions<T> grid) where T : class
		{
			Expression<Func<T, object>>[] properySpecifiers = typeof (T).GetProperties()
				.Where(info => ShouldPropertyBeDisplayed(info))
				.Select(info => ProperyToLamdaExpression<T>(info)).ToArray();

			grid.Columns(
				builder => { properySpecifiers.ForEach(propertySpecifier => builder.For(propertySpecifier)); });
			return grid;
		}

		private static bool ShouldPropertyBeDisplayed(PropertyInfo info)
		{
			return info.CanRead && info.PropertyType != typeof (Guid) && info.PropertyType != typeof (Guid?);
		}

		private static Expression<Func<T, object>> ProperyToLamdaExpression<T>(PropertyInfo info)
		{
			ParameterExpression param = Expression.Parameter(typeof (T), "arg");
			MemberExpression member = Expression.Property(param, info);
			UnaryExpression loselyTypeExpression = Expression.Convert(member, typeof (object));
			return Expression.Lambda<Func<T, object>>(loselyTypeExpression, param);
		}

		public class DropDownListItem<T>
		{
			public T Value { get; set; }
			public string DisplayName { get; set; }
		}

		public static string ActionButton<TController>(this HtmlHelper helper, string linkText,
		                                               Expression<Func<TController, object>> actionExpression)
		{
			string controllerName = typeof (TController).GetControllerName();
			string actionName = actionExpression.GetActionName();
			return
				string.Format(
					"<div class=\"buttonLeftEndCap\"></div><div class=\"buttonContentBackground\">{0}</div><div class=\"buttonRightEndCap\"></div>",
					helper.ActionLink(linkText, actionName, controllerName,helper.ViewContext.RouteData,null));
		}

		public static string RouteButton(this HtmlHelper helper, string linkText,
													   string routeName)
		{
			return
				string.Format(
					"<div class=\"buttonLeftEndCap\"></div><div class=\"buttonContentBackground\">{0}</div><div class=\"buttonRightEndCap\"></div>",
					helper.RouteLink(linkText, routeName));
					
		}
		public static string UrlButton(this HtmlHelper helper, string linkText,
													   string url)
		{
			return
				string.Format(
					"<div class=\"buttonLeftEndCap\"></div><div class=\"buttonContentBackground\">{0}</div><div class=\"buttonRightEndCap\"></div>",
					"<a href=\""+ url+"\">"+linkText+"</a>");

		}
	}
}