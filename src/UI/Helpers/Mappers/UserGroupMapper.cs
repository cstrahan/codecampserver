using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class UserGroupMapper : AutoInputMapper<UserGroup, UserGroupInput>, IUserGroupMapper
	{
	    private readonly IUserRepository _userRepository;

	    public UserGroupMapper(IUserGroupRepository repository, IUserRepository userRepository) : base(repository)
		{
		    _userRepository = userRepository;
		}

	    protected override Guid GetIdFromMessage(UserGroupInput input)
		{
			return input.Id;
		}

		protected override void MapToModel(UserGroupInput input, UserGroup model)
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
		    var existingUsers = model.GetUsers();
		    
            IEnumerable<User> usersToRemove = existingUsers.Where(user => !input.Users.Any(uf => uf.Id==user.Id) );
		    
            foreach (var user in usersToRemove)
		    {
                model.Remove(user);    
		    }

		    IEnumerable<UserSelectorInput> userFormToAdd = input.Users.Where(userForm => !existingUsers.Any(user => user.Id == userForm.Id));
		    var users = _userRepository.GetAll();

            foreach (var userForm in userFormToAdd)
		    {
		        User user = users.FirstOrDefault(user1 => user1.Id == userForm.Id);
		        model.Add(user);
		    }
		}
	}
}
