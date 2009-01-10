using CodeCampServer.Core.Common;
using CodeCampServer.Infrastructure.AutoMap;

namespace CodeCampServer.UI.Models.CustomResolvers
{
	public abstract class BaseFormatter<T> : IValueFormatter
	{
		public string FormatValue(ResolutionContext context)
		{
			if (context.SourceValue == null)
				return null;

			if (!(context.SourceValue is T))
				return context.SourceValue.ToNullSafeString();

			return FormatValueCore((T) context.SourceValue);
		}

		protected abstract string FormatValueCore(T value);
	}
}