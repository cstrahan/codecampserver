using System;
using System.Web;
using StructureMap;

namespace CodeCampServer.Infrastructure.DataAccess
{
	public class UnitOfWorkModule : IHttpModule
	{
		private HttpApplication _context;

		public void Init(HttpApplication context)
		{
			_context = context;
			context.BeginRequest += context_BeginRequest;
			context.EndRequest += context_EndRequest;
		}

		private void context_BeginRequest(object sender, EventArgs e)
		{
			var instance = ObjectFactory.GetInstance<IUnitOfWork>();
			instance.Begin();
		}

		private void context_EndRequest(object sender, EventArgs e)
		{

			
		}

		public void Dispose()
		{
		}
	}
}