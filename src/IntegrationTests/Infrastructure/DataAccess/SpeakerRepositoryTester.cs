using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class SpeakerRepositoryTester : KeyedRepositoryTester<Speaker, ISpeakerRepository>
	{
		protected override ISpeakerRepository CreateRepository()
		{
			return new SpeakerRepository(GetSessionBuilder());
		}
	}
}