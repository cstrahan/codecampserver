using System;

namespace UITestHelper
{
	public static class UITestExceptionFactory
	{
		public static void AssertEquals(object expected, object toCheck, string message)
		{
			if (expected != toCheck)
				throw new Exception(string.Format("{0}. Values are not equal, expected '{1}' but got '{2}'.", message, expected,
				                                  toCheck));
		}
	}
}