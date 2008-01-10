using System.Web;
using CodeCampServer.Model;
using NHibernate;

namespace CodeCampServer.DataAccess.Impl
{
    public class NHibernateSessionModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.EndRequest += new System.EventHandler(context_EndRequest);
        }

        void context_EndRequest(object sender, System.EventArgs e)
        {
            HybridSessionBuilder builder = new HybridSessionBuilder();
            ISession session = builder.GetExistingWebSession();
            if (session != null)
            {
				Log.Debug(this, "Disposing of ISession " + session.GetHashCode());
                session.Dispose();
            }
        }

        public void Dispose()
        {
            
        }
    }
}