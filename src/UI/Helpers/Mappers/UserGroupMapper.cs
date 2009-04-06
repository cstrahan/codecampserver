using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class UserGroupMapper : AutoFormMapper<UserGroup, UserGroupForm>, IUserGroupMapper
	{
	    private readonly IUserRepository _userRepository;

	    public UserGroupMapper(IUserGroupRepository repository, IUserRepository userRepository) : base(repository)
		{
		    _userRepository = userRepository;
		}

	    protected override Guid GetIdFromMessage(UserGroupForm form)
		{
			return form.Id;
		}

		protected override void MapToModel(UserGroupForm form, UserGroup model)
		{
		    model.DomainName = form.DomainName;
			model.Key = form.Key;
			model.Id = form.Id;
			model.Name = form.Name;
		    model.HomepageHTML = form.HomepageHTML;
		    model.City = form.City;
		    model.Region = form.Region;
		    model.Country = form.Country;
		    model.GoogleAnalysticsCode = form.GoogleAnalysticsCode;
		    var existingUsers = model.GetUsers();
		    
            IEnumerable<User> usersToRemove = existingUsers.Where(user => !form.Users.Any(uf => uf.Id==user.Id) );
		    
            foreach (var user in usersToRemove)
		    {
                model.Remove(user);    
		    }

		    IEnumerable<UserSelector> userFormToAdd = form.Users.Where(userForm => !existingUsers.Any(user => user.Id == userForm.Id));
		    var users = _userRepository.GetAll();

            foreach (var userForm in userFormToAdd)
		    {
		        User user = users.FirstOrDefault(user1 => user1.Id == userForm.Id);
		        model.Add(user);
		    }
		}
	}
}
