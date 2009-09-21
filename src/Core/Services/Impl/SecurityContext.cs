using System;
using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services.Impl
{
	public class SecurityContext : ISecurityContext
	{
		private readonly IUserSession _session;
		private readonly IUserGroupRepository _repository;

		public SecurityContext(IUserSession session, IUserGroupRepository repository)
		{
			_session = session;
			_repository = repository;
		}

		public virtual bool HasPermissionsFor(UserGroup usergroup)
		{
			User user = _session.GetCurrentUser();

			if (usergroup != null && usergroup.GetUsers().Any(user1 => user1.Username == user.Username))
			{
				return true;
			}
			return IsAdmin();
		}

		public virtual bool HasPermissionsForUserGroup(Guid Id)
		{
			return HasPermissionsFor(_repository.GetById(Id));
		}

		public bool IsAdmin()
		{
			User user = _session.GetCurrentUser();
			UserGroup defaultUserGroup = _repository.GetDefaultUserGroup();
			if (defaultUserGroup.GetUsers().Any(user1 => user1.Username == user.Username))
			{
				return true;
			}
			return false;
		}

		public bool HasPermissionsFor(Session session)
		{
			return HasPermissionsFor(session.Conference);
		}

	    public bool HasPermissionsFor(Meeting meeting)
	    {
	        return HasPermissionsFor(meeting.UserGroup);
	    }


	    public bool HasPermissionsFor(Speaker speaker)
		{
			return HasPermissionsFor(speaker.Conference);
		}

		public virtual bool HasPermissionsFor(Conference conference)
		{
			return HasPermissionsFor(conference.UserGroup);
		}
	}
}