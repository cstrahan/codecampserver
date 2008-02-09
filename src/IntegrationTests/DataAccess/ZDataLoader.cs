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
                                "http://microsoft.com/");
                Sponsor sponsor2 = new Sponsor("Central Market", "http://www.centralmarket.com/images/about/cmILoveFood.jpg", "http://www.centralmarket.com/");
                codeCamp2008.AddSponsor(sponsor, SponsorLevel.Platinum);
                codeCamp2008.AddSponsor(sponsor2, SponsorLevel.Gold);

                session.SaveOrUpdate(codeCamp2008);

                TimeSlot slot1 = codeCamp2008.AddTimeSlot(new DateTime(2008, 11, 26, 19, 30, 00),
                                                          new DateTime(2008, 11, 26, 20, 30, 00));
                TimeSlot slot2 = codeCamp2008.AddTimeSlot(new DateTime(2008, 11, 26, 21, 00, 00),
                                                          new DateTime(2008, 11, 26, 22, 00, 00));
                TimeSlot slot3 = codeCamp2008.AddTimeSlot(new DateTime(2008, 11, 26, 22, 30, 00),
                                                          new DateTime(2008, 11, 26, 23, 30, 00));
                Speaker speaker1 = new Speaker("Homer", "Simpson", "http://www.simpsons.com", "Doh!", codeCamp2008,
                    "homer@simpsons.com", "Homer J Simpson", "http://www.simpsons.com/homer.jpg", "Homer Simpson's Profiles", getPassword(), getSalt());
                Speaker speaker2 = new Speaker("Frank", "Sinatra", "http://www.sinatra.com", "Old Blue Eyes", codeCamp2008,
                    "frank@sinatra.com", "Frank Sinatra", "http://www.sinatra.com/frank_sinatra.jpg", "Frank Sinatra's Profile", getPassword(), getSalt());
                Session session1 = new Session(speaker1, "Domain-driven design explored", "In this session we'll explore Domain-driven design");
                Session session2 = new Session(speaker1, "Advanced NHibernate", "In this session we'll explore Advanced NHibernate");
                Session session3 = new Session(speaker2, "Extreme Programming: a primer", "In this session we'll provide a primer on XP");
                slot1.Session = session1;
                slot2.Session = session2;
                slot3.Session = session3;

            	for (int i = 0; i < 100; i++)
            	{
            		Attendee attendee = createAttendee(codeCamp2008, i.ToString().PadLeft(3, '0'));
					session.SaveOrUpdate(attendee);
            	}
            
                session.SaveOrUpdate(speaker1);
                session.SaveOrUpdate(speaker2);
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
