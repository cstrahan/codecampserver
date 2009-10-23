using System;
using AutoMapper;

namespace CodeCampServer.Infrastructure.ObjectMapping.CustomResolvers
{
	public class NullValueResolver : IValueResolver
	{

		public ResolutionResult Resolve(ResolutionResult source)
		{
			return new ResolutionResult(null);
		}
	}
}