using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CodeCampServer.Core.Domain.Model.Enumerations;

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
			InputSpecification.Helper.ViewData[InputSpecification.InputName] = selectList.Where(item => item.Selected).Select(item => item.Value).FirstOrDefault();
			return InputSpecification.Helper.DropDownList(InputSpecification.InputName, selectList,
			                                              InputSpecification.CustomAttributes);
		}


		private IEnumerable<SelectListItem> GetSelectList()
		{
			IEnumerable<SelectListItem> selectList =
				HtmlExtensions.GetSelectListForDropDown(EnumerationHelper.GetAll(InputSpecification.PropertyInfo.PropertyType),
				                                        false, GetSelectedValue(), null);
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