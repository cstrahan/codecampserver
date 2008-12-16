using System.Collections;
using System.Collections.Generic;

namespace RegressionTests
{
    public static class BaseClassLibraryExtensions
    {
        public static bool IsEmpty(this IEnumerable enumerable) 
        {
            foreach (var o in enumerable)
            {
                return false;
            }
            return true;
        }
    }
}