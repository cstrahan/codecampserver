using System;
using System.Reflection;

namespace CodeCampServer.Website.Helpers
{
    public static class ReflectionExtensions
    {
        public static bool HasAttribute<T>(this Type type, string memberName) where T : Attribute
        {
            return GetAttribute<T>(type, memberName) != null;
        }

        public static T GetAttribute<T>(this Type type, string memberName) where T: Attribute
        {
            var members = type.GetMember(memberName);

            if (members.Length == 0) throw new ArgumentException(string.Format("Member '{0}' not found on type {1}", memberName, type.Name));
            if (members.Length > 1) throw new AmbiguousMatchException(string.Format("More than 1 member '{0}' found on type {1}", memberName, type.Name));

            return members[0].GetAttribute<T>();
        }

        public static T GetAttribute<T>(this MemberInfo member) where T : Attribute
        {
            var attributeType = typeof(T);
            var attributes = member.GetCustomAttributes(attributeType, true);

            if (attributes.Length == 0)
                return null;

            return (T)attributes[0];
        }
    }
}