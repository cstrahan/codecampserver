using System;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.Core.Services.Bases;
using CodeCampServer.Infrastructure.DataAccess;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class ChangeAuditInfoInterceptorTester : TestBase
	{
		[Test]
		public void Should_ignore_non_auditable_entities()
		{
			var userSession = S<IUserSession>();
			var currentUser = new User();
			userSession.Stub(us => us.GetCurrentUser()).Return(currentUser);

			var testEntity = S<PersistentObject>();

			var interceptor = new ChangeAuditInfoInterceptor(userSession, null);

			interceptor.OnSave(testEntity, null, new[] {new ChangeAuditInfo()}, new[] {"ChangeAuditInfo"}, null);
			interceptor.OnFlushDirty(testEntity, null, new[] {new ChangeAuditInfo()}, null, new[] {"ChangeAuditInfo"}, null);
		}

		[Test]
		public void Should_tag_created_and_updated_info_when_no_created_date_exists()
		{
			var userSession = S<IUserSession>();
			var currentUser = new User();
			userSession.Stub(us => us.GetCurrentUser()).Return(currentUser);

			var conference = new Conference();

			var interceptor = new ChangeAuditInfoInterceptor(userSession, new ClockStub(new DateTime(2008, 10, 20)));

			interceptor.OnSave(conference, null, new[] {new ChangeAuditInfo()}, new[] {"ChangeAuditInfo"}, null);

			conference.ChangeAuditInfo.Created.ShouldEqual(new DateTime(2008, 10, 20));
			conference.ChangeAuditInfo.CreatedBy.ShouldEqual(currentUser);
			conference.ChangeAuditInfo.Updated.ShouldEqual(new DateTime(2008, 10, 20));
			conference.ChangeAuditInfo.UpdatedBy.ShouldEqual(currentUser);
		}

		[Test]
		public void Should_tag_updated_info_when_created_info_exists()
		{
			var userSession = S<IUserSession>();
			var createdUser = new User();
			var currentUser = new User();
			userSession.Stub(us => us.GetCurrentUser()).Return(currentUser);

			var conference = new Conference
			             	{ChangeAuditInfo = new ChangeAuditInfo {Created = new DateTime(2008, 10, 1), CreatedBy = createdUser}};

			var interceptor = new ChangeAuditInfoInterceptor(userSession, new ClockStub(new DateTime(2008, 10, 20)));

			interceptor.OnFlushDirty(conference, null, new[] {conference.ChangeAuditInfo}, null, new[] {"ChangeAuditInfo"}, null);

			conference.ChangeAuditInfo.Created.ShouldEqual(new DateTime(2008, 10, 1));
			conference.ChangeAuditInfo.CreatedBy.ShouldEqual(createdUser);
			conference.ChangeAuditInfo.Updated.ShouldEqual(new DateTime(2008, 10, 20));
			conference.ChangeAuditInfo.UpdatedBy.ShouldEqual(currentUser);
		}
	}
}