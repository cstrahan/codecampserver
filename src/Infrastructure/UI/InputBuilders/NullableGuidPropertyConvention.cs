using System;
using System.Reflection;

namespace CodeCampServer.Infrastructure.UI.InputBuilders
{
	public class NullableGuidPropertyConvention:InputBuilderPropertyConvention
	{
		public override bool CanHandle(PropertyInfo propertyInfo)
		{
			return typeof(Guid?).IsAssignableFrom(propertyInfo.PropertyType);
		}
		public override string PartialNameConvention(PropertyInfo propertyInfo)
		{
			return "Guid";
		}
		public override string Layout()
		{
			return "HiddenField";
		}
	}
}