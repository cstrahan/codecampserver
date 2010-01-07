using System;
using System.Collections.Generic;
using System.Reflection;

namespace CodeCampServer.Core.Domain.Bases
{
    [Serializable]
    public abstract class ValueObject<T> : IEquatable<T> where T : class
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as T;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            const int startValue = 17;
            const int multiplier = 59;

            int hashCode = startValue;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                    hashCode = hashCode*multiplier + value.GetHashCode();
            }

            return hashCode;
        }

        public virtual bool Equals(T other)
        {
            if (other == null)
                return false;

            Type t = GetType();

            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                object value1 = field.GetValue(other);
                object value2 = field.GetValue(this);

                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (value2 == null)
                {
                    return false;
                }
                else if ((typeof (DateTime).IsAssignableFrom(field.FieldType)) ||
                         ((typeof (DateTime?).IsAssignableFrom(field.FieldType))))
                {
                    string dateString1 = ((DateTime) value1).ToLongDateString();
                    string dateString2 = ((DateTime) value2).ToLongDateString();
                    if (!dateString1.Equals(dateString2))
                    {
                        return false;
                    }
                    continue;
                }
                else if (!value1.Equals(value2))
                    return false;
            }

            return true;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            Type t = GetType();

            var fields = new List<FieldInfo>();

            while (t != typeof (object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));

                t = t.BaseType;
            }

            return fields;
        }

        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);

            return x.Equals(y);
        }

        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            return !(x == y);
        }
    }
}