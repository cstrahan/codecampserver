using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class SpeakerRepositoryTester : KeyedRepositoryTester<Speaker, ISpeakerRepository>
	{
	    [Test]
	    public void Should_retrieve_all_speakers_for_a_conference()
	    {
            var conference = new Conference();

	        using(var session =GetSession())
	        {	            
	            session.SaveOrUpdate(conference);
	            session.SaveOrUpdate(new Speaker());
                session.SaveOrUpdate(new Speaker(){Conference = conference});
                session.SaveOrUpdate(new Speaker() { Conference = conference });
                session.Flush();
	        }
	        var repository = CreateRepository();
	        Speaker[] speakers = repository.GetAllForConference(conference);
	        speakers.Length.ShouldEqual(2);
	    }

		protected override ISpeakerRepository CreateRepository()
		{
			return new SpeakerRepository(GetSessionBuilder());
		}
	}
}