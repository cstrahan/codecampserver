using System;
using System.ComponentModel;
using System.Globalization;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Helpers
{
	public class UserSelectorInputTypeConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof (string))
				return true;
			
			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var returnValue = new UserSelectorInput();
			returnValue.Id = new Guid(value.ToString());
			return returnValue;
		}
	}
}