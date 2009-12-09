using System;
using AutoMapper;

namespace CodeCampServer.Infrastructure.ObjectMapping.CustomResolvers
{
	public abstract class BaseResolver<T, TReturn> : IValueResolver
	{
		public object Resolve(object model)
		{
			return ResolveCore((T) model);
		}

		protected abstract TReturn ResolveCore(T model);
	    
		public ResolutionResult Resolve(ResolutionResult source)
		{
			return new ResolutionResult(ResolveCore((T) source.Value));
		}
	}
}