using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;
using StructureMap;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
    [TestFixture]
	public class ConferenceRepositoryTester : KeyedRepositoryTester<Conference, ConferenceRepository>
    {
        private Conference CreateConference()
        {
            var conference = new Conference
                                 {
                                     Name = "sdf",
                                     Description = "description",
                                     StartDate = new DateTime(2008, 12, 2),
                                     EndDate = new DateTime(2008, 12, 3),
                                     LocationName = "St Edwards Professional Education Center",
                                     Address = "12343 Research Blvd",
                                     City = "Austin",
                                     Region = "Tx",
                                     PostalCode = "78234",
                                     PhoneNumber = "512-555-1234"
                                 };
            conference.AddAttendee(new Attendee {EmailAddress = "werwer@asdfasd.com"});
            return conference;
        }


        [Test]
        public void Should_remove_an_attendee_from_its_collection()
        {
            Conference conference = CreateConference();

            var repository = ObjectFactory.GetInstance<IConferenceRepository>();
            repository.Save(conference);
            conference.RemoveAttendee(conference.Attendees[0]);
            repository.Save(conference);

            Conference rehydratedConference;
            using (ISession session = GetSession())
            {
                rehydratedConference = session.Load<Conference>(conference.Id);
                rehydratedConference.Attendees.Length.ShouldEqual(0);
            }
        }

        [Test]
        public void Should_update_an_attendee()
        {
            Conference conference = CreateConference();
            conference.Attendees[0].Status = AttendanceStatus.NotAttending;

            var repository = ObjectFactory.GetInstance<IConferenceRepository>();

            repository.Save(conference);
            conference.Attendees[0].Status = AttendanceStatus.Confirmed;
            repository.Save(conference);

            Conference rehydratedConference = repository.GetById(conference.Id);
            rehydratedConference.Attendees[0].Status.ShouldEqual(AttendanceStatus.Confirmed);
        }
    }
}