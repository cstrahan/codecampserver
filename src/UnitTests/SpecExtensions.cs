using System;
using NBehave.Spec.NUnit;

namespace CodeCampServer.UnitTests
{
	public static class SpecExtensions
	{
		public static void ShouldBeOfLength(this Array array, int length)
		{
			array.Length.ShouldEqual(length);
		}
	}
}