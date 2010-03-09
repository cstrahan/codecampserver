using AutoMapper;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.CustomResolvers
{
	public class NullValueResolver : IValueResolver
	{
		public ResolutionResult Resolve(ResolutionResult source)
		{
			return new ResolutionResult(null);
		}
	}
}