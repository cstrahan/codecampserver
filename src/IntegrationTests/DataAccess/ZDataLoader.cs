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
            Conference codeCamp2007 =
                new Conference("codecamp2007", "Uber Code Camp 2007");
            codeCamp2007.StartDate = new DateTime(2007, 11, 26, 19, 30, 00);
            Conference codeCamp2008 = new Conference("codecamp2008", "Uber Code Camp 2008");
            codeCamp2008.StartDate = new DateTime(2008, 4, 13, 19, 0, 0);
            Conference codeCamp2009 = new Conference("codecamp2009", "Uber Code Camp 2009");
            codeCamp2009.StartDate = new DateTime(2009, 6, 8, 19, 0, 0);

            Attendee attendee1 =
                new Attendee("Homer", "Simpson", "http://www.simpsons.com", "Doh!", codeCamp2007, "a@b.com");
            Attendee attendee2 =
                new Attendee("Bart", "Simpson", "http://www.simpsons.com", "Eat my shorts", codeCamp2007,
                             "a@b.com");
            Attendee attendee3 =
                new Attendee("Marge", "Simpson", "http://www.simpsons.com", "MMmmmm", codeCamp2007, "a@b.com");

            using (ISession session = getSession(Database.Default))
            {
                session.SaveOrUpdate(codeCamp2007);
                session.SaveOrUpdate(codeCamp2008);
                session.SaveOrUpdate(codeCamp2009);
                session.SaveOrUpdate(attendee1);
                session.SaveOrUpdate(attendee2);
                session.SaveOrUpdate(attendee3);

                session.Flush();
            }
        }

        [Test, Category("CreateSchema")]
        public void RecreateDatabaseSchema()
        {
            recreateDatabase(Database.Default);
        }
    }
}