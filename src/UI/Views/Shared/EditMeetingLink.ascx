<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<IKeyable>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
<form method="get" class="editButtonForm" action="<%=CodeCampServer.UI.Helpers.Extensions.UrlHelperExtensions.Action<MeetingController>(Url, t=>t.Edit((Meeting)null, null)) %>">
	<%=	Html.Hidden("Meeting", Model.Key) %>
	<button class="ui-state-default ui-corner-all fg-button-icon-solo" type="submit"
		title="Edit Meeting" >
	<span class="ui-icon ui-icon-pencil" />
</button>
</form>

<%}%>