using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using CodeCampServer.DataAccess.Impl;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.IntegrationTests.DataAccess
{
    [TestFixture]
    public class SessionRepositoryTester : DatabaseTesterBase
    {
        [Test]
        public void ShouldSaveSessionToDatabase()
        {
            Conference conference = new Conference("austincodecamp", "");
            Person speaker = new Person("first", "last", "email@email.com");
            using (NHibernate.ISession dataSession = getSession())
            {
                dataSession.SaveOrUpdate(conference);
                dataSession.SaveOrUpdate(speaker);
                dataSession.Flush();
            }

            Session newSession = new Session(conference, speaker, "title", "abstract", null);

            ISessionRepository repository = new SessionRepository(_sessionBuilder);
            repository.Save(newSession);

            // Get Session back from database to ensure it was saved correctly
            using (NHibernate.ISession dataSession = getSession())
            {
                Session rehydratedSession = dataSession.Load<Session>(newSession.Id);

                Assert.That(rehydratedSession != null);
                Assert.That(rehydratedSession, Is.EqualTo(newSession));
                Assert.That(rehydratedSession.Speaker, Is.EqualTo(speaker));
                Assert.That(rehydratedSession.Title, Is.EqualTo("title"));
                Assert.That(rehydratedSession.Abstract, Is.EqualTo("abstract"));
				Assert.That(rehydratedSession.Track, Is.Null);
            }
        }

        [Test]
        public void ShouldGetUnallocatedApprovedSessions()
        {
            Conference conference = new Conference("austincodecamp", "");
            Person speaker = new Person("first", "last", "email@email.com");

            // Make one session and approve it so it will be returned
            Session approvedSession = new Session(conference, speaker, "Unallocated", "Abstract");
            approvedSession.IsApproved = true;

            // Make one session uannproved so it won't be returned 
            Session proposedSession = new Session(conference, speaker, "Proposed", "Abstract");
            proposedSession.IsApproved = false;
            
            // Make one session and approve it and allocate it so it won't be returned
            Session approvedAllocatedSession = new Session(conference, speaker, "Allocated", "Abstract");
            approvedAllocatedSession.IsApproved = true;

            TimeSlot timeSlot = new TimeSlot(conference, DateTime.Now, DateTime.Now.AddHours(1), "Session");
            timeSlot.AddSession(approvedAllocatedSession);

            using (NHibernate.ISession dataSession = getSession())
            {
                dataSession.SaveOrUpdate(conference);
                dataSession.SaveOrUpdate(speaker);
                dataSession.SaveOrUpdate(proposedSession);
                dataSession.SaveOrUpdate(approvedSession);
                dataSession.SaveOrUpdate(approvedAllocatedSession);
                dataSession.SaveOrUpdate(timeSlot);
                dataSession.Flush();
            }

            ISessionRepository repository = new SessionRepository(_sessionBuilder);
            List<Session> sessions = new List<Session>(repository.GetUnallocatedApprovedSessions(conference));
            // Make sure we only got the one Proposed session
            Assert.That(sessions.Count, Is.EqualTo(1));
            Assert.That(sessions[0].Title, Is.EqualTo("Unallocated"));
            // Ensure conference is retrieved as well
            Assert.IsNotNull(sessions[0].Conference);
        }

        [Test]
        public void ShouldGetProposedSessions()
        {
            Conference conference = new Conference("austincodecamp", "");
            Person speaker = new Person("first", "last", "email@email.com");
            // Make one session that should be returned
            Session proposedSession = new Session(conference, speaker, "Proposed", "Abstract");
            proposedSession.IsApproved = false;
            // Make one session and approve it so it will NOT be returned
            Session approvedSession = new Session(conference, speaker, "Scheduled", "Abstract");
            approvedSession.IsApproved = true;
            
            using (NHibernate.ISession dataSession = getSession())
            {
                dataSession.SaveOrUpdate(conference);
                dataSession.SaveOrUpdate(speaker);
                dataSession.SaveOrUpdate(proposedSession);
                dataSession.SaveOrUpdate(approvedSession);
                dataSession.Flush();
            }

            ISessionRepository repository = new SessionRepository(_sessionBuilder);
            List<Session> sessions = new List<Session>(repository.GetProposedSessions(conference));
            // Make sure we only got the one Proposed session
            Assert.That(sessions.Count, Is.EqualTo(1));
            Assert.That(sessions[0].Title, Is.EqualTo("Proposed"));
        }
    }
}