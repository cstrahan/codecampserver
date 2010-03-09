using System.Linq;
using AutoMapper;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.CustomResolvers
{
	public class UserToUserSelectorInputResolver : IValueResolver
	{
		public ResolutionResult Resolve(ResolutionResult source)
		{
			var users = (User[]) source.Value;

			var inputs = users.Select(user => Mapper.Map<User, UserInput>(user)).ToList();

			return
				new ResolutionResult(inputs);
		}
	}
}