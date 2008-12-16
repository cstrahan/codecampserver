using System;

namespace CodeCampServer.UI.Models.Forms.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class LabelAttribute : Attribute
	{
		private readonly string _value;

		public LabelAttribute(string value)
		{
			_value = value;
		}

		public string Value
		{
			get { return _value; }
		}
	}
}