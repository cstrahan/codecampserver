using System.Web.Mvc;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.UI.Services;
using CodeCampServer.UI.Services;

namespace CodeCampServer.UI.Helpers.Filters
{
	public class AssemblyVersionFilterAttribute : ActionFilterAttribute
	{
		public const string AssemblyVersion = "AssemblyVersion";
		private IAssemblyVersion _assemblyVersion;
		public AssemblyVersionFilterAttribute() : this(DependencyRegistrar.Resolve<IAssemblyVersion>()) { }

		public AssemblyVersionFilterAttribute(IAssemblyVersion assemblyVersionService)
		{
			_assemblyVersion = assemblyVersionService;
		}


		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			string version = _assemblyVersion.GetAssemblyVersion();

			AddTheVersionToTheViewData(filterContext, version);
		}

		private void AddTheVersionToTheViewData(ActionExecutingContext filterContext, string referrer)
		{
			filterContext.Controller.ViewData.Add(AssemblyVersion, referrer);
		}

	}
}