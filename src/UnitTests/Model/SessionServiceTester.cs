using System;
using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Model
{
    [TestFixture]
    public class SessionServiceTester
    {
        [Test]
        public void CreatingNewSessionShouldSaveSessionToRepository()
        {
            MockRepository mocks = new MockRepository();

            ISessionRepository repository = mocks.CreateMock<ISessionRepository>();
            Session actualSession = null;
            repository.Save(null);
            LastCall.IgnoreArguments().Do(new Action<Session>(delegate(Session obj) { actualSession = obj; }));
            mocks.ReplayAll();

            ISessionService service = new SessionService(repository);
            Speaker speaker = new Speaker("a", "b", "c", "d", new Conference(), "e", "f", "g", "h", "password", "salt");
            List<OnlineResource> resources = new List<OnlineResource>();
            resources.Add(new OnlineResource(OnlineResourceType.Blog, "Name", "http://myblog.com"));

            Session session = service.CreateSession(speaker, "title", "abstract", null, resources.ToArray());

            mocks.VerifyAll();

            Assert.That(actualSession, Is.EqualTo(session));
            Assert.That(actualSession.Speaker, Is.EqualTo(speaker));
            Assert.That(actualSession.Title, Is.EqualTo("title"));
            Assert.That(actualSession.Abstract, Is.EqualTo("abstract"));
            Assert.That(actualSession.GetResources(), Is.EqualTo(resources.ToArray()));
			Assert.That(actualSession.Track, Is.Null);
        }
    }
}
