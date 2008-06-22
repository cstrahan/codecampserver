using System;
using CodeCampServer.DataAccess.Impl;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using NHibernate;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.DataAccess
{
	[TestFixture]
	public class ZDataLoader : DatabaseTesterBase
	{
		[Test, Category("DataLoader")]
		public void PopulateDatabase()
		{
			using (ISession session = getSession())
			{
				ITransaction transaction = session.BeginTransaction();

				var admin = new Person("Admin", "", "admin@admin.com");
				SetPassword(admin, "admin");
				session.SaveOrUpdate(admin);

				var codeCamp2008 = new Conference("austincodecamp2008", "Austin Code Camp 2008");
				codeCamp2008.StartDate = new DateTime(2008, 11, 26, 19, 30, 00);
				codeCamp2008.PubliclyVisible = true;

				var microsoft =
					new Sponsor("Microsoft",
					            "http://www.microsoft.com/presspass/images/gallery/logos/thumbnails/mslogo-1.gif",
					            "http://microsoft.com/", "Bill", "Gates", "billg@microsoft.com", SponsorLevel.Platinum);

				var visualsvn = new Sponsor("VisualSVN", "/content/images/sponsors/visualsvn.png", "http://www.visualsvn.com/",
				                            "Visual", "SVN", "sales@visualsvn.com", SponsorLevel.Platinum);

				var centralmarket =
					new Sponsor("Central Market", "http://www.centralmarket.com/images/about/cmILoveFood.jpg",
					            "http://www.centralmarket.com/", "H. E.", "Butts",
					            "owner@centralmarket.com", SponsorLevel.Gold);

				codeCamp2008.AddSponsor(microsoft);
				codeCamp2008.AddSponsor(centralmarket);
				codeCamp2008.AddSponsor(visualsvn);

				session.SaveOrUpdate(codeCamp2008);
				transaction.Commit();

				// Start a new transaction
				session.Clear();
				transaction = session.BeginTransaction();

				// Add track(s) and time slot(s)
				var track1 = new Track(codeCamp2008, ".NET");
				var track2 = new Track(codeCamp2008, "Web");
				var slot1 = new TimeSlot(codeCamp2008, new DateTime(2008, 11, 26, 19, 30, 00),
				                         new DateTime(2008, 11, 26, 20, 30, 00),
				                         "Session");
				var slot1_2break = new TimeSlot(codeCamp2008, new DateTime(2008, 11, 26, 20, 30, 00),
				                                new DateTime(2008, 11, 26, 21, 00, 00),
				                                "Break");
				var slot2 = new TimeSlot(codeCamp2008, new DateTime(2008, 11, 26, 21, 00, 00),
				                         new DateTime(2008, 11, 26, 22, 00, 00),
				                         "Session");
				var slot2_3break = new TimeSlot(codeCamp2008, new DateTime(2008, 11, 26, 22, 00, 00),
				                                new DateTime(2008, 11, 26, 22, 30, 00),
				                                "Break");
				var slot3 = new TimeSlot(codeCamp2008, new DateTime(2008, 11, 26, 22, 30, 00),
				                         new DateTime(2008, 11, 26, 23, 30, 00),
				                         "Session");


				var person1 = new Person("Homer", "Simpson", "homer@simpson.com");
				var person2 = new Person("Frank", "Sinatra", "frank@sinatra.com");

				codeCamp2008.AddSpeaker(person1, "hsimpson", "bio", "avatar");
				codeCamp2008.AddSpeaker(person2, "fsinatra", "bio", "avatar");

				var session1 = new Session(codeCamp2008, person1, "Domain-driven design explored",
				                           "In this session we'll explore Domain-driven design", track1);
				var session2 = new Session(codeCamp2008, person1, "Advanced NHibernate",
				                           "In this session we'll explore Advanced NHibernate", track1);
				var session3 = new Session(codeCamp2008, person2, "NHibernate for Beginners",
				                           "In this session we'll help Aaron Lerch understand NHibernate", track2);
				var session4 = new Session(codeCamp2008, person2, "Extreme Programming: a primer",
				                           "In this session we'll provide a primer on XP",
				                           track1);

				slot1.AddSession(session1);
				slot2.AddSession(session2);
				slot2.AddSession(session3);
				slot3.AddSession(session4);

				for (int i = 0; i < 100; i++)
				{
					createAttendee(session, codeCamp2008, i.ToString().PadLeft(3, '0'));
				}

				session.SaveOrUpdate(person1);
				session.SaveOrUpdate(person2);
				session.SaveOrUpdate(codeCamp2008);
				session.SaveOrUpdate(session1);
				session.SaveOrUpdate(session2);
				session.SaveOrUpdate(session3);
				session.SaveOrUpdate(session4);
				session.SaveOrUpdate(track1);
				session.SaveOrUpdate(track2);
				session.SaveOrUpdate(slot1);
				session.SaveOrUpdate(slot1_2break);
				session.SaveOrUpdate(slot2);
				session.SaveOrUpdate(slot2_3break);
				session.SaveOrUpdate(slot3);

				transaction.Commit();

				IConferenceService service = getConferenceService();
				service.RegisterAttendee("Jeffrey", "Palermo", "jeffreypalermo@yahoo.com", "http://www.jeffreypalermo.com",
				                         "comment", codeCamp2008, "password");
			}
		}

		private static IConferenceService getConferenceService()
		{
			return new ConferenceService(
				new ConferenceRepository(new HybridSessionBuilder()),
				new Cryptographer(),
				new SystemClock()
				);
		}

		private static void createAttendee(ISession session, Conference conference, string suffix)
		{
			Person person = createPerson(suffix);
			session.SaveOrUpdate(person);
			conference.AddAttendee(person);
		}

		private static Person createPerson(string suffix)
		{
			var person = new Person("Homer" + suffix, "Simpson" + suffix, "homer" + suffix + "@simpson.com");
			SetPassword(person, "password");

			return person;
		}

		private static void SetPassword(Person person, string password)
		{
			var cryptographyService = new Cryptographer();
			person.PasswordSalt = cryptographyService.CreateSalt();
			person.Password = cryptographyService.HashPassword(password, person.PasswordSalt);
		}

		[Test, Category("CreateSchema"), Explicit]
		public void RecreateDatabaseSchema()
		{
			recreateDatabase();
		}
	}
}