using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using NHibernate;
using StructureMap;

namespace CodeCampServer.DataAccess.Impl
{
	[Pluggable(Keys.DEFAULT)]
	public class TrackRepository : RepositoryBase, ITrackRepository
	{
        public TrackRepository(ISessionBuilder sessionFactory)
            : base(sessionFactory)
		{
		}

        public void Save(Track track)
        {
            ISession session = getSession();
            session.SaveOrUpdate(track);
            session.Flush();
        }

        public Track[] GetTracksFor(Conference conference)
        {
            IQuery query = getSession().CreateQuery(
                @"from Track t join fetch t.Conference 
                 where t.Conference = ? order by t.Name");
            query.SetParameter(0, conference, NHibernateUtil.Entity(typeof(Conference)));
            return new List<Track>(query.List<Track>()).ToArray();
        }
    }
}
