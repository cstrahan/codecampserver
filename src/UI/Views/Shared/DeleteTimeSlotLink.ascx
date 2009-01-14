<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
	var timeslot = (TimeSlotForm) ViewData.Model; %>
		<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
				<a title="Delete <%= timeslot.GetName() %>" href="<%= Url.Action<TimeSlotController>(t => t.Delete(null), new{timeslot = timeslot.Id}).ToXHTMLLink() %>"><img src="/images/icons/application_delete.png" /></a>
		<%}%>		
