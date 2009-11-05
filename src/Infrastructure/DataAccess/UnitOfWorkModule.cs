using System;
using System.Web;
using StructureMap;

namespace CodeCampServer.Infrastructure.DataAccess
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
			var instance = ObjectFactory.GetInstance<IUnitOfWork>();
			instance.Begin();
		}

		private void context_EndRequest(object sender, EventArgs e)
		{
			var instance = ObjectFactory.GetInstance<IUnitOfWork>();
			try
			{
				instance.Commit();
			}
			finally
			{
				instance.Dispose();				
			}			
		}
	}
}