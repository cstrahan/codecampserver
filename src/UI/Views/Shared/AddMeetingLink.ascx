<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated)
{%>
    <%= Html.ImageLink<MeetingController>(x=>x.New(null), "~/images/icons/application_add.png", "Add a new meeting") %>
<%}%>
