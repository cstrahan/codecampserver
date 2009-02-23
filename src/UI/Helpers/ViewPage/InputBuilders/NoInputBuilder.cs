using CodeCampServer.Core.Common;
using CodeCampServer.UI.Helpers.Attributes;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public class NoInputBuilder : BaseInputBuilder
	{
		public override bool IsSatisfiedBy(IInputSpecification specification)
		{
			return specification.PropertyInfo.HasCustomAttribute<NoInputAttribute>();
		}

		protected override string CreateInputElementBase()
		{
			return (GetValue() ?? "").ToString();
		}
	}
}