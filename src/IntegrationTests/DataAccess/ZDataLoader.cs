using System;
using CodeCampServer.DataAccess;
using CodeCampServer.DataAccess.Impl;
using CodeCampServer.Domain.Model;
using NHibernate;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.DataAccess
{
    [TestFixture]
    public class ZDataLoader : RepositoryBase
    {
        public ZDataLoader(ISessionBuilder sessionFactory) : base(sessionFactory)
        {
        }

        [Test, Category("DataLoader")]
        public void PopulateDatabase()
        {
            TestHelper.EmptyDatabase();
            Conference devTeachVancouver2007 = new Conference("DevTeach2007Vancouver", "Party with Palermo: DevTeach 2007 Edition - Vancouver");
            devTeachVancouver2007.StartDate = new DateTime(2007, 11, 26, 19, 30, 00);
            Conference mvpSummit2008 = new Conference("MvpSummit2008", "Party with Palermo: MVP Summit 2008 Edition");
            mvpSummit2008.StartDate = new DateTime(2008, 4, 13, 19, 0, 0);
            Conference techEd2008 = new Conference("TechEd2008", "Party with Palermo: Tech Ed 2008 Edition");
            techEd2008.StartDate = new DateTime(2008, 6, 8, 19, 0, 0);

            Attendee attendee1 = new Attendee("Homer", "Simpson", "http://www.simpsons.com", "Doh!", devTeachVancouver2007, "a@b.com");
            Attendee attendee2 = new Attendee("Bart", "Simpson", "http://www.simpsons.com", "Eat my shorts", devTeachVancouver2007, "a@b.com");
            Attendee attendee3 = new Attendee("Marge", "Simpson", "http://www.simpsons.com", "MMmmmm", devTeachVancouver2007, "a@b.com");

            using (ISession session = getSession(Database.Default))
            {
                session.SaveOrUpdate(devTeachVancouver2007);
                session.SaveOrUpdate(mvpSummit2008);
                session.SaveOrUpdate(techEd2008);
                session.SaveOrUpdate(attendee1);
                session.SaveOrUpdate(attendee2);
                session.SaveOrUpdate(attendee3);

                session.Flush();
            }
        }
    }
}