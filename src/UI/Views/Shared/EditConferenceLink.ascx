<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<ConferenceInput>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
    <%= Html.ImageLink<ConferenceController>(
            t=>t.Edit((Conference)null), new{Conference = Model.Key},
            "~/images/icons/application_edit.png", "Edit the conference") %>
<%}%>