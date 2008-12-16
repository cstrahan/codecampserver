using System.Collections;

namespace RegressionTests
{
    public static class BaseClassLibraryExtensions
    {
        public static bool IsEmpty(this IEnumerable enumerable)
        {
            foreach (object o in enumerable)
            {
                return false;
            }
            return true;
        }
    }
}