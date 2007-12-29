using System;
using CodeCampServer.DataAccess;
using CodeCampServer.Model.Domain;
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
            using (ISession session = getSession(Database.Default))
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
                Speaker speaker = new Speaker("Homer", "Simpson", "http://www.simpsons.com", "Doh!", codeCamp2008, "a@b.com", "somelink");
                Session session1 = new Session(speaker, "Domain-driven design explored");
                Session session2 = new Session(speaker, "Advanced NHibernate");
                Session session3 = new Session(speaker, "Extreme Programming: a primer");
                slot1.Session = session1;
                slot2.Session = session2;
                slot3.Session = session3;
            
                session.SaveOrUpdate(speaker);
                session.SaveOrUpdate(session1);
                session.SaveOrUpdate(session2);
                session.SaveOrUpdate(session3);
                transaction.Commit();
            }
        }

        [Test, Category("CreateSchema")]
        public void RecreateDatabaseSchema()
        {
            recreateDatabase(Database.Default);
        }
    }
}