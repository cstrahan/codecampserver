using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
    public interface IUserRepository
    {
        User GetByUserName(string username);
        void Save(User user);
        User GetById(Guid id);
        User[] GetAll();
        User[] GetLikeLastNameStart(string query);
    }
}