using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	public class SpeakerRepositoryTester : KeyedRepositoryTester<Speaker, ISpeakerRepository>
	{
		protected override ISpeakerRepository CreateRepository()
		{
			return new SpeakerRepository(GetSessionBuilder());
		}
	}
}