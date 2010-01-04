using System;

namespace CodeCampServer.UI.Helpers.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class HiddenAttribute : Attribute
	{
	}


}