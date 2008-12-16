using System.Reflection;

namespace CodeCampServer.UI.Views
{
	public interface IInputElementAuthority
	{
		bool Permits(PropertyInfo propertyInfo);
		bool Forbids(PropertyInfo propertyInfo);
	}
}