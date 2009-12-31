<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<SponsorInput>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers.Extensions"%>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>

<form method="get" class="editButtonForm" action="<%=Url.Action("Edit", new { sponsorID = Model.Id }) %>">
	<%=	Html.Hidden("sponsorID", Model.Id) %>
	<button class="ui-state-default ui-corner-all fg-button-icon-solo" type="submit"
		title="Edit  Sponsor" >
	<span class="ui-icon ui-icon-pencil" />
</button>
</form>
