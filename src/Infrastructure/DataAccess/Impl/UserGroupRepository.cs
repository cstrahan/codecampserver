using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
    public class UserGroupRepository:KeyedRepository<UserGroup>,IUserGroupRepository
    {
        public UserGroupRepository(ISessionBuilder sessionFactory) : base(sessionFactory) {}
    }
}