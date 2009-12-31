<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
<form method="get" class="editButtonForm" action="<%=Url.Action<MeetingController>(x=>x.New(null)) %>">
	<button class="ui-state-default ui-corner-all fg-button-icon-solo" type="submit"
		title="Add Meeting" >
	<span class="ui-icon ui-icon-plus" />
</button>
</form>

<%}%>
