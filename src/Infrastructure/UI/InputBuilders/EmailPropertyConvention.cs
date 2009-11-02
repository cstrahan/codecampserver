using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CodeCampServer.UI.Helpers.Attributes;
using MvcContrib.UI.InputBuilder.Conventions;

namespace CodeCampServer.Infrastructure.UI.InputBuilders
{
	public class EmailPropertyConvention:InputBuilderPropertyConvention
	{
		public override bool CanHandle(PropertyInfo propertyInfo)
		{
			if(propertyInfo.AttributeExists<DataTypeAttribute>())
			{
				return propertyInfo.GetAttribute<DataTypeAttribute>().DataType == DataType.EmailAddress;
			}
			return false;
		}
		public override string PartialNameConvention(PropertyInfo propertyInfo)
		{
			return "String";
		}
	}
}