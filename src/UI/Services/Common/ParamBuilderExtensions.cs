using System;
using MvcContrib.UI.ParamBuilder;

namespace CodeCampServer.UI.Services.Common
{
	public static class ParamBuilderExtensions
	{
		public static ParamBuilder ViewAsPdf(this ParamBuilder builder)
		{
			return ViewAsPdf(builder, 1);
		}

		public static ParamBuilder User(this ParamBuilder builder, Guid id)
		{
			return builder.Add(ParamNames.User, id);
		}

		public static ParamBuilder UserGroup(this ParamBuilder builder, string key)
		{
			return builder.Add(ParamNames.UserGroup, key);
		}

		public static ParamBuilder ViewAsPdf(this ParamBuilder builder, int value)
		{
			return builder.Add(ParamNames.ViewAsPdf, value);
		}
	}
}