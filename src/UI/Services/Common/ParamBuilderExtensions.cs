using MvcContrib.UI.ParamBuilder;

namespace CodeCampServer.UI.Services.Common
{
	public static class ParamBuilderExtensions
	{
		public static ParamBuilder ViewAsPdf(this ParamBuilder builder)
		{
			return ViewAsPdf(builder, 1);
		}

		public static ParamBuilder ViewAsPdf(this ParamBuilder builder, int value)
		{
			return builder.Add(ParamNames.ViewAsPdf, value);
		}
	}
}