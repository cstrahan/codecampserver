using System;

namespace CodeCampServer.UI.Helpers.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class CheckboxListAttribute : Attribute
	{
		public CheckboxListAttribute(Type selectListProvider)
		{
			SelectListProvider = selectListProvider;
		}

		public Type SelectListProvider { get; set; }
	}
}