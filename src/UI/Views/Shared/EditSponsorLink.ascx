<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<SponsorInput>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers.Extensions"%>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>

<%= Html.EditImageButton(Url.Action("Edit", new { sponsorID = Model.Id }))%>