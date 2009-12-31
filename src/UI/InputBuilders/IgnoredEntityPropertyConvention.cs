using System.Reflection;
using CodeCampServer.Core.Bases;

namespace CodeCampServer.UI.InputBuilders
{
	public class IgnoredEntityPropertyConvention : InputBuilderPropertyConvention
	{
		public override bool CanHandle(PropertyInfo propertyInfo)
		{
			return typeof (PersistentObject).IsAssignableFrom(propertyInfo.PropertyType);
		}

		public override string PartialNameConvention(PropertyInfo propertyInfo)
		{
			return "EmptyInputBuilder";
		}
	}
}