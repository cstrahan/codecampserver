using System;

namespace CodeCampServer.UI.Models.AutoMap
{
	public abstract class TypeMember
	{
		public abstract object GetValue(object obj);
		public abstract Type GetMemberType();
	}
}