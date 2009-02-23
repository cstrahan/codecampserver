using System;
using CodeCampServer.Core.Domain;

namespace CodeCampServer.UI.Helpers.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class UserDropDownAttribute : Attribute
	{
	}
}