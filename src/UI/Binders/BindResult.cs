using System.Globalization;
using System.Web.Mvc;

namespace CodeCampServer.UI.Binders
{
	public class BindResult
	{
		public object Value { get; private set; }
		public ValueProviderResult ValueProviderResult { get; private set; }

		public BindResult(object value, ValueProviderResult valueProviderResult)
		{
			Value = value;
			ValueProviderResult = valueProviderResult ?? new ValueProviderResult(null, string.Empty, CultureInfo.CurrentCulture);
		}
	}
}