using System.Reflection;
using CodeCampServer.UI.Views;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class InputElementAuthority : IInputElementAuthority
	{
		public bool Permits(PropertyInfo propertyInfo)
		{
			return true;
		}

		public bool Forbids(PropertyInfo propertyInfo)
		{
			return !Permits(propertyInfo);
		}
	}
}