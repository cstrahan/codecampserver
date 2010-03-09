using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models;
using MvcContrib;

namespace CodeCampServer.UI.Helpers.Filters
{
	public class AddPageInfoForMasterPage : ContainerBaseActionFilter, IConventionActionFilter
	{
		public override void  OnActionExecuted(ActionExecutedContext filterContext)
		{
 				var ViewData = filterContext.Controller.ViewData;
			PageInfo pageInfo = null;

			if (!ViewData.Contains<PageInfo>())
			{
				pageInfo = new PageInfo { Title = "Code Camp Server v1.0" };
				ViewData.Add(pageInfo);
			}
			else
			{
				pageInfo = ViewData.Get<PageInfo>();
			}

			if (ViewData.Contains<UserGroup>())
			{
				var usergroup = ViewData.Get<UserGroup>();
				pageInfo.Title = usergroup.Name;

				if (!usergroup.IsDefault())
					pageInfo.TrackingCode = usergroup.GoogleAnalysticsCode;
			}

			if (ViewData.Contains<Conference>())
			{
				pageInfo.SubTitle = ViewData.Get<Conference>().Name;
			}

		}
	}
}