<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<UserGroupInput>" %>
<%@ Import Namespace="CodeCampServer.Core.Common" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated)
  {%>
<form method="get" class="editButtonForm" action="<%=Url.Action<UserGroupController>(t => t.Edit(Guid.Empty)) %>">
<%=	Html.Hidden("entityToEdit", Model.Id) %>
<button class="ui-state-default ui-corner-all fg-button-icon-solo" type="submit"
	title="Edit User Group">
	<span class="ui-icon ui-icon-pencil" />
</button>
</form>
<%}%>