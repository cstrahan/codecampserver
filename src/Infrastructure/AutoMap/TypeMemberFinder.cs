using System.Reflection;
using CodeCampServer.Core.Common;

namespace CodeCampServer.Infrastructure.AutoMap
{
	public class TypeMemberFinder
	{
		public static TypeMember FindTypeMember(PropertyInfo[] modelProperties, MethodInfo[] getMethods, string nameToSearch)
		{
			PropertyInfo pi = ReflectionHelper.FindModelPropertyByName(modelProperties, nameToSearch);
			if (pi != null)
				return new PropertyMember(pi);

			MethodInfo mi = ReflectionHelper.FindModelMethodByName(getMethods, nameToSearch);
			if (mi != null)
				return new MethodMember(mi);

			return null;
		}
	}
}