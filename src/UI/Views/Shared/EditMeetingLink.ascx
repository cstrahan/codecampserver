<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<IKeyable>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
<a href="<%=Url.Action<MeetingController>(t=>t.Edit((Meeting)null,null), new{Meeting = Model.Key}) %>"><img src="/images/icons/application_edit.png" /></a>
<%}%>