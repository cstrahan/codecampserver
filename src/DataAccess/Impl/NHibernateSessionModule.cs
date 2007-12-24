using System.Web;
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
                session.Dispose();
            }
        }

        public void Dispose()
        {
            
        }
    }
}