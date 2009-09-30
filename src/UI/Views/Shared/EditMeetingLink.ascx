<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<MeetingInput>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
    <%= Html.ImageLink<MeetingController>(
            t=>t.Edit((Meeting)null,null), new{Meeting = Model.Key},
            "~/images/icons/application_edit.png", "Edit the meeting") %>
<%}%>