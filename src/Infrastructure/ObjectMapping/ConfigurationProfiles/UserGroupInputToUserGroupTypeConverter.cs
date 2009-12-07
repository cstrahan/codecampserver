using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.UI.Mappers
{
	public class UserGroupInputToUserGroupTypeConverter:IUserGroupInputToUserGroupTypeConverter
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserGroupRepository _userGroupRepository;

		public UserGroupInputToUserGroupTypeConverter(IUserRepository userRepository,IUserGroupRepository userGroupRepository)
		{
			_userRepository = userRepository;
			_userGroupRepository = userGroupRepository;
		}

		public void UpdateUserGroupFromInput(UserGroup model, UserGroupInput input)
		{
			model.DomainName = input.DomainName;
			model.Key = input.Key;
			model.Id = input.Id;
			model.Name = input.Name;
			model.HomepageHTML = input.HomepageHTML;
			model.City = input.City;
			model.Region = input.Region;
			model.Country = input.Country;
			model.GoogleAnalysticsCode = input.GoogleAnalysticsCode;
			User[] existingUsers = model.GetUsers();

			IEnumerable<User> usersToRemove = existingUsers.Where(user => !input.Users.Any(uf => uf.Id == user.Id));

			foreach (User user in usersToRemove)
			{
				model.Remove(user);
			}

			IEnumerable<UserSelectorInput> userFormToAdd =
				input.Users.Where(userForm => !existingUsers.Any(user => user.Id == userForm.Id));
			User[] users = _userRepository.GetAll();

			foreach (UserSelectorInput userForm in userFormToAdd)
			{
				User user = users.FirstOrDefault(user1 => user1.Id == userForm.Id);
				model.Add(user);
			}
		}

		public UserGroup Convert(UserGroupInput source)
		{
			UserGroup destination = _userGroupRepository.GetById(source.Id) ?? new UserGroup();
			UpdateUserGroupFromInput(destination,source);
			return destination;
		}
	}
}