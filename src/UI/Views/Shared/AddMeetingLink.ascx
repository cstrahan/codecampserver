<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
<a href="<%=Url.Action<MeetingController>(x=>x.New(null)) %>"><img src="/images/icons/application_add.png" alt="Add a Meeting" /></a>
<%}%>
