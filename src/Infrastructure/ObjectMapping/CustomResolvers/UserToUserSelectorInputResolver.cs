using System.Linq;
using AutoMapper;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.ObjectMapping.TypeConverters
{
	public class UserToUserSelectorInputResolver : IValueResolver {
		public ResolutionResult Resolve(ResolutionResult source)
		{
			User[] users = (User[]) source.Value;

			var inputs =users.Select(user => Mapper.Map<User, UserInput>(user)).ToList();

			return
				new ResolutionResult( inputs );
		}
	}
}