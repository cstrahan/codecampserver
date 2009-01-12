using System;
using System.Text;
using System.Web.Mvc.Html;
using CodeCampServer.Core.Common;
using CodeCampServer.UI.Models.Forms.Attributes;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public class HiddenInputBuilder : BaseInputBuilder
	{
		protected override string CreateInputElementBase()
		{
			return InputSpecification.Helper.Hidden(InputSpecification.InputName);
		}

		protected override void AppendLabel(StringBuilder output)
		{
			return;
		}

		public override bool IsSatisfiedBy(IInputSpecification specification)
		{
			return specification.PropertyInfo.HasCustomAttribute<HiddenAttribute>() ||
				   specification.PropertyInfo.PropertyType == typeof(Guid) || specification.PropertyInfo.PropertyType == typeof(Guid?);
		}

		protected override void AppendCleaner(StringBuilder output)
		{
			return;
		}
	}
}