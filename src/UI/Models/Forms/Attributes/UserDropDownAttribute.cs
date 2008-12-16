using System;
using CodeCampServer.Core.Domain;

namespace CodeCampServer.UI.Models.Forms.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class UserDropDownAttribute : Attribute
	{
	}
}