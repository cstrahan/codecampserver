using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
    public class UserGroupRepository:KeyedRepository<UserGroup>,IUserGroupRepository
    {
        public UserGroupRepository(ISessionBuilder sessionFactory) : base(sessionFactory) {}
        public UserGroup GetByDomainName(string domainName)
        {
            return GetSession().CreateQuery(
                       "from UserGroup usergroup where usergroup.DomainName = :domainname ").SetString("domainname",
                                                                                                       domainName).
                       UniqueResult < UserGroup>();
                //.SetDateTime("today", DateTime.Now.Midnight()).SetMaxResults(1).UniqueResult<Conference>();
            
        }
    }
}