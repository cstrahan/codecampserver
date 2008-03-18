using System;
using CodeCampServer.DataAccess;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using NHibernate;
using NUnit.Framework;
using StructureMap;

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
                Conference codeCamp2008 =
                    new Conference("austincodecamp2008", "Austin Code Camp 2008");
                codeCamp2008.StartDate = new DateTime(2008, 11, 26, 19, 30, 00);

                Sponsor sponsor =
                    new Sponsor("Microsoft",
                                "http://www.microsoft.com/presspass/images/gallery/logos/thumbnails/mslogo-1.gif",
                                "http://microsoft.com/", "Bill", "Gates", "billg@microsoft.com", SponsorLevel.Platinum);
                Sponsor sponsor2 =
                    new Sponsor("Central Market", "http://www.centralmarket.com/images/about/cmILoveFood.jpg",
                                "http://www.centralmarket.com/", "H. E.", "Butts", "owner@centralmarket.com", SponsorLevel.Gold);
                codeCamp2008.AddSponsor(sponsor);
                codeCamp2008.AddSponsor(sponsor2);

                session.SaveOrUpdate(codeCamp2008);
                transaction.Commit();

                // Start a new transaction
                session.Clear();
                transaction = session.BeginTransaction();

                // Add track(s) and time slot(s)
                Track track = new Track(codeCamp2008, ".NET");
                TimeSlot slot1 = new TimeSlot(codeCamp2008, new DateTime(2008, 11, 26, 19, 30, 00),
                                              new DateTime(2008, 11, 26, 20, 30, 00),
                                              "Session");
                TimeSlot slot2 = new TimeSlot(codeCamp2008, new DateTime(2008, 11, 26, 21, 00, 00),
                                              new DateTime(2008, 11, 26, 22, 00, 00),
                                              "Session");
                TimeSlot slot3 = new TimeSlot(codeCamp2008, new DateTime(2008, 11, 26, 22, 30, 00),
                                              new DateTime(2008, 11, 26, 23, 30, 00),
                                              "Session");

                
                Person person1 = new Person("Homer", "Simpson", "homer@simpson.com");
                Person person2 = new Person("Frank", "Sinatra", "frank@sinatra.com");

                codeCamp2008.AddSpeaker(person1, "hsimpson", "bio", "avatar");
                codeCamp2008.AddSpeaker(person2, "fsinatra", "bio", "avatar");

                Session session1 = new Session(codeCamp2008, person1, "Domain-driven design explored",
                                               "In this session we'll explore Domain-driven design", track);
                Session session2 = new Session(codeCamp2008, person1, "Advanced NHibernate",
                                               "In this session we'll explore Advanced NHibernate", track);
                Session session3 = new Session(codeCamp2008, person2, "NHibernate for Beginners",
                                               "In this session we'll help Aaron Lerch understand NHibernate", track);
                Session session4 = new Session(codeCamp2008, person2, "Extreme Programming: a primer",
                                               "In this session we'll provide a primer on XP",
                                               track);

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
                session.SaveOrUpdate(track);
                session.SaveOrUpdate(slot1);
                session.SaveOrUpdate(slot2);
                session.SaveOrUpdate(slot3);
                transaction.Commit();
                
                IConferenceService service = ObjectFactory.GetInstance<IConferenceService>();
                service.RegisterAttendee("Jeffrey", "Palermo", "jeffreypalermo@yahoo.com", "http://www.jeffreypalermo.com", "comment", codeCamp2008, "password");
            }
        }

        private string getSalt()
        {
            return "4OVv7LLaf/R29CXZK+LiFCjCEnmxfCUnvRUOl70GIeFD83JjPL26o/lSkIOanwXUUq+S9gfp9ycD1otbjpqSYg==";
        }

        private string getPassword()
        {
            return "UyRkzg7mm/W1zAlR/1Euph+Z1E8="; //hash for "password" with default salt.
        }

        private void createAttendee(ISession session, Conference conference, string suffix)
        {
            Person person = createPerson(suffix);     
            session.SaveOrUpdate(person);
            conference.AddAttendee(person);
        }

        private Person createPerson(string suffix)
        {
            Person person = new Person("Homer" + suffix, "Simpson" + suffix, "homer" + suffix + "@simpson.com");
            person.Password = getPassword();
            person.PasswordSalt = getSalt();

            return person;
        }

        [Test, Category("CreateSchema"), Explicit]
        public void RecreateDatabaseSchema()
        {
            recreateDatabase(Database.Default);
        }
    }
}
