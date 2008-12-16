using NHibernate;
using Tarantino.Core.Commons.Services.Logging;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess
{
	public class JcmsSessionBuilder : HybridSessionBuilder
	{
		private readonly IdCacheInterceptor _idCacheInterceptor;
		public static bool UseImportInterceptor;

		public JcmsSessionBuilder()
		{
			_idCacheInterceptor = new IdCacheInterceptor();
			Logger.Debug(this, "Constructed");
		}

		protected override ISession OpenSession(ISessionFactory factory)
		{
			if (UseImportInterceptor)
			{
				Logger.Debug(this,
				             string.Format("Openning session with interceptor '{0}' for session factory '{1}'.",
				                           _idCacheInterceptor.GetType().Name,
				                           factory.GetHashCode()));

				return factory.OpenSession(_idCacheInterceptor);
			}

			return factory.OpenSession();
		}
	}
}