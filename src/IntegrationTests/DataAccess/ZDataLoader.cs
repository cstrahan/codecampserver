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
                session.SaveOrUpdate(codeCamp2008);

                TimeSlot slot1 = codeCamp2008.AddTimeSlot(new DateTime(2008, 11, 26, 19, 30, 00),
                                                          new DateTime(2008, 11, 26, 20, 30, 00));
                TimeSlot slot2 = codeCamp2008.AddTimeSlot(new DateTime(2008, 11, 26, 21, 00, 00),
                                                          new DateTime(2008, 11, 26, 22, 00, 00));
                TimeSlot slot3 = codeCamp2008.AddTimeSlot(new DateTime(2008, 11, 26, 22, 30, 00),
                                                          new DateTime(2008, 11, 26, 23, 30, 00));
                Speaker speaker = new Speaker("Homer", "Simpson", "http://www.simpsons.com", "Doh!", codeCamp2008,
                    "a@b.com", "HomerSimpson", "somelink", "profile info", getPassword(), getSalt());
                Session session1 = new Session(speaker, "Domain-driven design explored");
                Session session2 = new Session(speaker, "Advanced NHibernate");
                Session session3 = new Session(speaker, "Extreme Programming: a primer");
                slot1.Session = session1;
                slot2.Session = session2;
                slot3.Session = session3;

            	for (int i = 0; i < 100; i++)
            	{
            		Attendee attendee = createAttendee(codeCamp2008, i.ToString().PadLeft(3, '0'));
					session.SaveOrUpdate(attendee);
            	}
            
                session.SaveOrUpdate(speaker);
                session.SaveOrUpdate(session1);
                session.SaveOrUpdate(session2);
                session.SaveOrUpdate(session3);
                transaction.Commit();

				IConferenceService service = ObjectFactory.GetInstance<IConferenceService>();
				service.RegisterAttendee("Jeffrey", "Palermo", "http://www.jeffreypalermo.com", "comment",
										 codeCamp2008, "jeffreypalermo@yahoo.com", "password");
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

    	private Attendee createAttendee(Conference conference, string suffix)
    	{
    		Attendee attendee = new Attendee("Homer" + suffix, "Simpson" + suffix, "http://www.simpsons.com" + suffix,
                "I'll be there with " + suffix, conference, "homer" + suffix + "@simpsons.com", getPassword(), getSalt());
    		return attendee;
    	}

    	[Test, Category("CreateSchema"), Explicit]
        public void RecreateDatabaseSchema()
        {
            recreateDatabase(Database.Default);
        }
    }
}
