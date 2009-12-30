using System.Reflection;
using CodeCampServer.UI.Helpers.Attributes;
using MvcContrib.UI.InputBuilder.Conventions;

namespace CodeCampServer.UI.InputBuilders
{
	public class MultiLinePropertyConvention:InputBuilderPropertyConvention
	{
		public override bool CanHandle(PropertyInfo propertyInfo)
		{
			return propertyInfo.AttributeExists<MultilineAttribute>();
		}
		public override string PartialNameConvention(PropertyInfo propertyInfo)
		{
			return "MultilineText";
		}
	}
}