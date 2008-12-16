using System;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
    public interface IConferenceRepository
    {
        void Save(Conference conference);
        Conference GetById(Guid id);
        Conference[] GetAll();
    }
}