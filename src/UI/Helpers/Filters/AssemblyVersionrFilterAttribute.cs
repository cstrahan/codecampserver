using System.Web.Mvc;
using CodeCampServer.UI.Services;

namespace CodeCampServer.UI.Helpers.Filters
{
	public class AssemblyVersionFilterAttribute : ContainerBaseActionFilter,IConventionActionFilter
	{
		public const string AssemblyVersion = "AssemblyVersion";
		private IAssemblyVersion _assemblyVersion;

		public AssemblyVersionFilterAttribute() 
		{
			_assemblyVersion = CreateDependency <IAssemblyVersion>();
		}

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