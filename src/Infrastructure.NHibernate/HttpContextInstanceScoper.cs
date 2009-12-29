using System.Collections;
using System.Web;

namespace CodeCampServer.Infrastructure.NHibernate
{
	public class HttpContextInstanceScoper<T> : InstanceScoperBase<T>
	{
		public bool IsEnabled()
		{
			return GetHttpContext() != null;
		}

		private HttpContext GetHttpContext()
		{
			return HttpContext.Current;
		}

		protected override IDictionary GetDictionary()
		{
			return GetHttpContext().Items;
		}
	}
}