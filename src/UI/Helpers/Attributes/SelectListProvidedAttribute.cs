using System;

namespace CodeCampServer.UI.Helpers.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class SelectListProvidedAttribute : Attribute
	{
		public Type SelectListProvider { get; set; }

		public SelectListProvidedAttribute(Type selectListProvider)
		{
			SelectListProvider = selectListProvider;
		}
	}
}