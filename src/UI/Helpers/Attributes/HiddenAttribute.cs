using System;

namespace CodeCampServer.UI.Models.Forms.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class HiddenAttribute : Attribute
	{
	}
}