<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<SponsorInput>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
    <%= Html.ImageLink<SponsorController>(
            t=>t.Edit(null,(SponsorInput)null), new{sponsorID = Model.ID},
            "~/images/icons/application_edit.png", "Edit the conference") %>
<%}%>