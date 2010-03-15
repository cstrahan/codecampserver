using System;
using CodeCampServer.Core.Domain.Bases;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	public class HeartbeatRepositoryTester : RepositoryTester<Heartbeat, IHeartbeatRepository>
	{
		protected override Heartbeat CreateValidInput()
		{
			return new Heartbeat{Date = new DateTime(2008,4,5)};
		}

		[Test]
		public void should_get_latest()
		{
			var h1 = new Heartbeat {Date = new DateTime(2001, 5, 4), Message = "Not it"};
			var h2 = new Heartbeat {Date = new DateTime(2002, 4, 3), Message = "Not it"};
			var h3 = new Heartbeat {Date = new DateTime(2003, 3, 2), Message = "Not it"};
			var h4 = new Heartbeat {Date = new DateTime(2004, 2, 1), Message = "Pick Me"};

			PersistEntities(h2, h4, h1, h3);

			var latest = CreateRepository().GetLatest();
			latest.ShouldEqual(h4);
		}

		[Test]
		public void should_limit_top()
		{
			CheckQueryIsLimited(x => x.GetTop());
		}

		[Test]
		public void should_order_top()
		{
			var h1 = new Heartbeat { Date = new DateTime(2001, 5, 4), Message = "Not it" };
			var h2 = new Heartbeat { Date = new DateTime(2002, 4, 3), Message = "Not it" };
			var h3 = new Heartbeat { Date = new DateTime(2003, 3, 2), Message = "Not it" };
			var h4 = new Heartbeat { Date = new DateTime(2004, 2, 1), Message = "Pick Me" };

			PersistEntities(h2, h4, h1, h3);

			var top = CreateRepository().GetTop();
			top[0].ShouldEqual(h4);
			top[1].ShouldEqual(h3);
			top[2].ShouldEqual(h2);
			top[3].ShouldEqual(h1);
		}

		[Test]
		public void should_not_limit_all()
		{
			CheckQueryIsNotLimited(x => x.GetAll());
		}
	}
}