using System;
using AutoMapper;

namespace CodeCampServer.UI.Models.CustomResolvers
{
	public class NullValueResolver : IValueResolver
	{

	    public ResolutionResult Resolve(ResolutionResult source)
	    {
	        return new ResolutionResult(null);
	    }
	}
}