using System;
using CodeCampServer.DataAccess.Impl;
using CodeCampServer.Model.Domain;
using Iesi.Collections.Generic;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System.Collections.Generic;

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
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(conference);
                session.SaveOrUpdate(speaker);
                session.Flush();
            }

            List<OnlineResource> resources = new List<OnlineResource>();
            resources.Add(new OnlineResource(OnlineResourceType.Blog, "My Blog", "http://www.myblog.com"));
            Session newSession = new Session(speaker, "title", "abstract", resources.ToArray());

            ISessionRepository repository = new SessionRepository(_sessionBuilder);
            repository.Save(newSession);

            Session rehydratedSession = null;
            //get Session back from database to ensure it was saved correctly
            using (ISession session = getSession())
            {
                rehydratedSession = session.Load<Session>(newSession.Id);

                Assert.That(rehydratedSession != null);
                Assert.That(rehydratedSession, Is.EqualTo(newSession));
                Assert.That(rehydratedSession.Speaker, Is.EqualTo(speaker));
                Assert.That(rehydratedSession.Title, Is.EqualTo("title"));
                Assert.That(rehydratedSession.Abstract, Is.EqualTo("abstract"));
                Assert.That(rehydratedSession.GetResources(), Is.EqualTo(resources.ToArray()));
            }
        }
    }
}
