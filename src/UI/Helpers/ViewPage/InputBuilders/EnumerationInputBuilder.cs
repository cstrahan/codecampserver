using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CodeCampServer.Core.Domain.Model.Enumerations;
using Tarantino.Core.Commons.Model.Enumerations;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public class EnumerationInputBuilder : BaseInputBuilder
	{
		public override bool IsSatisfiedBy(IInputSpecification specification)
		{
			return (typeof (Enumeration)).IsAssignableFrom(specification.PropertyInfo.PropertyType);
		}

		protected override string CreateInputElementBase()
		{
			IEnumerable<SelectListItem> selectList = GetSelectList();
			return InputSpecification.Helper.DropDownList(InputSpecification.InputName, selectList,
			                                              InputSpecification.CustomAttributes);
		}


		private IEnumerable<SelectListItem> GetSelectList()
		{
			IEnumerable<SelectListItem> selectList =
				HtmlExtensions.GetSelectListForDropDown(EnumerationHelper.GetAll(InputSpecification.PropertyInfo.PropertyType),
				                                        true, GetSelectedValue(), null);
			return selectList;
		}

		private int? GetSelectedValue()
		{
			object value = GetValue();

			if (value != null) return ((Enumeration) value).Value;
			return null;
		}
	}
}