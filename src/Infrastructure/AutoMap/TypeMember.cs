using System;

namespace CodeCampServer.Infrastructure.AutoMap
{
	public abstract class TypeMember
	{
		public abstract object GetValue(object obj);
		public abstract Type GetMemberType();
	}
}