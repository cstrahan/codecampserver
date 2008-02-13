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
    public class TrackRepositoryTester : DatabaseTesterBase
    {
        [Test]
        public void ShouldSaveTrackToDatabase()
        {
            Conference conference = new Conference("austincodecamp", "");
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(conference);
                session.Flush();
            }

            Track track = new Track(conference, "Misc");

            ITrackRepository repository = new TrackRepository(_sessionBuilder);
            repository.Save(track);

            Track rehydratedTrack = null;
            //get Track back from database to ensure it was saved correctly
            using (ISession session = getSession())
            {
                rehydratedTrack = session.Load<Track>(track.Id);

                Assert.That(rehydratedTrack != null);
                Assert.That(rehydratedTrack, Is.EqualTo(track));
                Assert.That(rehydratedTrack.Conference, Is.EqualTo(conference));
                Assert.That(rehydratedTrack.Name, Is.EqualTo("Misc"));
            }
        }

        [Test]
        public void ExceptionIfTrackNameIsNotUnique()
        {
            Conference conference = new Conference("austincodecamp", "");
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(conference);
                session.Flush();
            }

            using (ISession session = getSession())
            {
                Track track1 = new Track(conference, "MISC");

                ITrackRepository repository = new TrackRepository(_sessionBuilder);
                repository.Save(track1);

                Track track2 = new Track(conference, "misc");

                bool exceptionThrownOnSave = false;

                try
                {
                    repository.Save(track2);
                }
                catch
                {
                    exceptionThrownOnSave = true;
                }

                Assert.IsTrue(exceptionThrownOnSave, "Exception should be thrown trying to save a second track with the same name--case insensitive");
            }
        }

        [Test]
        public void ShouldGetTracksForConference()
        {
            Conference conference = new Conference("austincodecamp", "");

            using (ISession session = getSession())
            {
                session.SaveOrUpdate(conference);
                session.Flush();
            }

            Track track1 = new Track(conference, "Misc");
            Track track2 = new Track(conference, ".NET");


            using (ISession trackSession = getSession())
            {
                trackSession.SaveOrUpdate(track1);
                trackSession.SaveOrUpdate(track2);
                trackSession.Flush();
            }

            ITrackRepository repository = new TrackRepository(_sessionBuilder);
            Track[] tracks = repository.GetTracksFor(conference);

            Assert.That(tracks.Length, Is.EqualTo(2));
            Assert.That(tracks[0].Name, Is.EqualTo(".NET"));
            Assert.That(tracks[1].Name, Is.EqualTo("Misc"));
        }
    }
}
