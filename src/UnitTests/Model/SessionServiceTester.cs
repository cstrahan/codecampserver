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
            Person speaker = new Person();

            Session session = service.CreateSession(null, speaker, "title", "abstract", null);

            mocks.VerifyAll();

            Assert.That(actualSession, Is.EqualTo(session));
            Assert.That(actualSession.Speaker, Is.EqualTo(speaker));
            Assert.That(actualSession.Title, Is.EqualTo("title"));
            Assert.That(actualSession.Abstract, Is.EqualTo("abstract"));
			Assert.That(actualSession.Track, Is.Null);
        }
    }
}
