using System;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
    public interface IPotentialAttendeeRepository
    {
        void Save(PotentialAttendee potentialAttendee);
        PotentialAttendee GetById(Guid id);
        PotentialAttendee[] GetAll();
    }
}