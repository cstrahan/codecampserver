using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;
using StructureMap;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class ConferenceRepositoryTester : KeyedRepositoryTester<Conference, ConferenceRepository>
	{
		protected override ConferenceRepository CreateRepository()
		{
			return (ConferenceRepository) ObjectFactory.GetInstance<IConferenceRepository>();
		}


		[Test]
		public void should_retrieve_conferences_for_a_usergroup()
		{
			var usergroup = new UserGroup();
			var conference1 = new Conference();
			var conference2 = new Conference();
			conference1.UserGroup = usergroup;

			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(usergroup);
				session.SaveOrUpdate(conference1);
				session.SaveOrUpdate(conference2);
				session.Flush();
			}

			IConferenceRepository repository = CreateRepository();
			Conference[] conferences = repository.GetAllForUserGroup(usergroup);

			conferences.Length.ShouldEqual(1);
		}

		[Test]
		public void Should_retrieve_upcoming_conferences_for_a_usergroup()
		{
			ObjectFactory.Inject(typeof (ISystemClock), new Clock(new DateTime(2009, 5, 5)));
			var usergroup = new UserGroup();
			var conference1 = new Conference
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 2),
			                  		EndDate = new DateTime(2009, 4, 6)
			                  	};
			var conference4 = new Conference
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 3),
			                  		EndDate = new DateTime(2009, 5, 4, 20, 0, 0)
			                  	};
			var conference2 = new Conference
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 4),
			                  		EndDate = new DateTime(2009, 5, 5, 20, 0, 0)
			                  	};
			var conference3 = new Conference
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 5),
			                  		EndDate = new DateTime(2009, 5, 7)
			                  	};

			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(usergroup);
				session.SaveOrUpdate(conference1);
				session.SaveOrUpdate(conference2);
				session.SaveOrUpdate(conference3);
				session.SaveOrUpdate(conference4);
				session.Flush();
			}

			IConferenceRepository repository = CreateRepository();
			Conference[] conferences = repository.GetFutureForUserGroup(usergroup);

			conferences.Length.ShouldEqual(2);
			conferences[0].ShouldEqual(conference2);
		}
	}
}