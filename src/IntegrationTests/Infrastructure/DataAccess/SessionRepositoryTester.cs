using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	public class SessionRepositoryTester : KeyedRepositoryTester<Session, SessionRepository>
	{
		protected override SessionRepository CreateRepository()
		{
			return new SessionRepository(GetSessionBuilder());
		}
	}
}