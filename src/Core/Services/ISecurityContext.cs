using System;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services
{
    public interface ISecurityContext
    {
        bool HasPermissionsFor(Speaker speaker);
        bool HasPermissionsFor(Conference conference);
        bool HasPermissionsFor(UserGroup usergroup);
        bool HasPermissionsForUserGroup(Guid Id);
        bool IsAdmin();
    }
}