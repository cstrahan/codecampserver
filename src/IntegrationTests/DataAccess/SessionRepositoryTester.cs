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
            Speaker speaker = new Speaker("first", "last", "http://google.com", "comment", conference, 
                                          "email@email.com", "display name", "http://avatars.com", 
                                          "http://profile.com", "password", "salt");
            using (NHibernate.ISession dataSession = getSession())
            {
                dataSession.SaveOrUpdate(conference);
                dataSession.SaveOrUpdate(speaker);
                dataSession.Flush();
            }

            List<OnlineResource> resources = new List<OnlineResource>();
            resources.Add(new OnlineResource(OnlineResourceType.Blog, "My Blog", "http://www.myblog.com"));
            Session newSession = new Session(speaker, "title", "abstract", resources.ToArray());

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
                Assert.That(rehydratedSession.GetResources(), Is.EqualTo(resources.ToArray()));
            }
        }

        [Test]
        public void ShouldGetProposedSessions()
        {
            Conference conference = new Conference("austincodecamp", "");
            Speaker speaker = new Speaker("first", "last", "http://google.com", "comment", conference,
                                          "email@email.com", "display name", "http://avatars.com",
                                          "http://profile.com", "password", "salt");
            // Make one session that should be returned
            Session proposedSession = new Session(speaker, "Proposed", "Abstract");
            proposedSession.IsApproved = false;
            // Make one session and approve it so it will NOT be returned
            Session approvedSession = new Session(speaker, "Scheduled", "Abstract");
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