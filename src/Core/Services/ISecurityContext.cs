using System;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services
{
	public interface ISecurityContext
	{
		bool HasPermissionsFor(UserGroup usergroup);
		bool HasPermissionsForUserGroup(Guid Id);
		bool IsAdmin();
	}
}