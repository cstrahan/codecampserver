using System;
using System.Web;


namespace CodeCampServer.Infrastructure.NHibernate.DataAccess
{
	public class UnitOfWorkModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.BeginRequest += context_BeginRequest;
			context.EndRequest += context_EndRequest;
		}

		public void Dispose()
		{

		}

		private void context_BeginRequest(object sender, EventArgs e)
		{
			//var instance = ObjectFactory.GetInstance<IUnitOfWork>();
		    var instance = UnitOfWorkFactory.Default();
			instance.Begin();
		}

		private void context_EndRequest(object sender, EventArgs e)
		{
			//var instance = ObjectFactory.GetInstance<IUnitOfWork>();
            var instance = UnitOfWorkFactory.Default();
            try
			{
				instance.Commit();
			}
			catch
			{
				instance.RollBack();
				throw;
			}
			finally
			{
				instance.Dispose();				
			}			
		}
	}
}