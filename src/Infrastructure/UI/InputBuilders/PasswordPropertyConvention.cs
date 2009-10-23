using System.Reflection;

namespace CodeCampServer.Infrastructure.UI.InputBuilders
{
	public class PasswordPropertyConvention:InputBuilderPropertyConvention
	{
		public override bool CanHandle(PropertyInfo propertyInfo)
		{
			return propertyInfo.Name.ToLower().Contains("password");
		}
		public override string PartialNameConvention(PropertyInfo propertyInfo)
		{
			return "Password";
		}
	}
}