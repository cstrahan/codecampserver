using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services.Impl;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services
{
	[TestFixture]
	public class HeartbeatCheckerTester : TestBase
	{
		[Test]
		public void should_report_success_when_heartbeat_is_within_the_specified_threshold()
		{
			var now = new DateTime(2010, 1, 3, 17, 2, 0);
			var nowMinusTwoMinutes = now.AddMinutes(-2);
			var heartbeat = new Heartbeat {Date = nowMinusTwoMinutes};
			var timeout = 5;

			var repo = S<IHeartbeatRepository>();
			repo.Stub(x => x.GetLatest()).Return(heartbeat);

			var clock = S<ISystemClock>();
			clock.Stub(x => x.Now()).Return(now);

			var checker = new HeartbeatChecker(repo, clock);
			checker.CheckHeartbeat(timeout).ShouldEqual(HeartbeatChecker.Success);
		}

		[Test]
		public void should_report_failure_when_there_are_zero_heartbeats()
		{
			var now = new DateTime(2010, 1, 3, 17, 2, 0);

			var repo = S<IHeartbeatRepository>();
			repo.Stub(x => x.GetLatest()).Return(null);

			var clock = S<ISystemClock>();
			clock.Stub(x => x.Now()).Return(now);

			var checker = new HeartbeatChecker(repo, clock);
			checker.CheckHeartbeat(5).ShouldEqual(HeartbeatChecker.DeadNoHeartbeat);
		}

		[Test]
		public void should_report_failure_when_the_last_heartbeat_is_too_old()
		{
			var now = new DateTime(2010, 1, 3, 17, 2, 0);
			var nowMinusTwoMinutes = now.AddMinutes(-2);
			var heartbeat = new Heartbeat { Date = nowMinusTwoMinutes };
			var timeout = 1;
			var failureMessage = string.Format("DEAD!  1 minute threshold exceeded.  Now [{0}], Last Heartbeat [{1}]", now,
			                                   nowMinusTwoMinutes);

			var repo = S<IHeartbeatRepository>();
			repo.Stub(x => x.GetLatest()).Return(heartbeat);

			var clock = S<ISystemClock>();
			clock.Stub(x => x.Now()).Return(now);

			var checker = new HeartbeatChecker(repo, clock);
			checker.CheckHeartbeat(timeout).ShouldEqual(failureMessage);
		}

		[Test]
		public void should_report_failure_when_the_last_heartbeat_is_in_the_future()
		{
			var now = new DateTime(2010, 1, 3, 17, 2, 0);
			var future = now.AddMinutes(2);
			var heartbeat = new Heartbeat { Date = future };
			var timeout = 5;
			var failureMessage = string.Format("DEAD!  Heartbeat is in the future.  Now [{0}], Last Heartbeat [{1}]", now,
			                                   future);

			var repo = S<IHeartbeatRepository>();
			repo.Stub(x => x.GetLatest()).Return(heartbeat);

			var clock = S<ISystemClock>();
			clock.Stub(x => x.Now()).Return(now);

			var checker = new HeartbeatChecker(repo, clock);
			checker.CheckHeartbeat(timeout).ShouldEqual(failureMessage);
		}
	}
}