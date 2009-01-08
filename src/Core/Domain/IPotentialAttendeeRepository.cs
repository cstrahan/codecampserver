using System;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
    public interface IPotentialAttendeeRepository
    {
        void Save(Attendee attendee);
        Attendee GetById(Guid id);
        Attendee[] GetAll();
        Attendee GetByEmail(string email);
    }
}