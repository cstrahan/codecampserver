using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.UI.Models.Forms;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
    {
        [TestFixture]
        public class ConferenceUpdaterTester:TestBase
        {
            [Test]
            public void Should_check_for_the_conference_to_exist()
            {

                IAttendeeMessage foo = S<IAttendeeMessage>();
                foo.ConferenceID = Guid.NewGuid();
                
                IConferenceRepository repository = M<IConferenceRepository>();
                repository.Stub(r => r.GetById(foo.ConferenceID)).Return(null);

                IConferneceUpdater updater = new ConferneceUpdater(repository);
                var result = updater.UpdateFromMessage(foo);
                result.Successful.ShouldBeFalse();
                result.GetMessages(m => m.ConferenceID)[0].ShouldEqual("Conference does not exist.");

                repository.AssertWasCalled(r => r.GetById(Guid.Empty), opt => opt.IgnoreArguments());
            }

            [Test]
            public void Should_check_for_unique_attendee_email_when_adding_new_attendee()
            {
                
                IAttendeeMessage foo = S<IAttendeeMessage>();
                foo.ConferenceID = Guid.NewGuid();
                foo.EmailAddress = "were@were.com";

                IConferenceRepository repository = M<IConferenceRepository>();
                var conference = new Conference();
                conference.AddAttendee(new Attendee(){EmailAddress = foo.EmailAddress});
                repository.Stub(r => r.GetById(foo.ConferenceID)).Return(conference);

                IConferneceUpdater updater = new ConferneceUpdater(repository);
                var result = updater.UpdateFromMessage(foo);
                result.Successful.ShouldBeFalse();
                result.GetMessages(m => m.EmailAddress)[0].ShouldEqual("Attended is already registered for this conference.");
                
                repository.AssertWasCalled(r=>r.GetById(Guid.Empty),opt=>opt.IgnoreArguments());
            }
            
        }
    }
