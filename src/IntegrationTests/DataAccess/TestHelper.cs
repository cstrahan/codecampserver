using System.Data;
using CodeCampServer.DataAccess;
using NHibernate;

namespace CodeCampServer.IntegrationTests.DataAccess
{
    public class TestHelper
    {
        public static void EmptyDatabase()
        {
            using (ISession session = NHibernateSessionFactory.GetSession())
            {
                using (IDbCommand command = session.Connection.CreateCommand())
                {
                    command.CommandText = "delete from attendees;delete from events";
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void RebuildDatabase()
        {
            
        }
    }
}