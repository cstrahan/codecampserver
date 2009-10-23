<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
    <%= Html.ImageLink<UserController>(
            t=>t.Edit((User)null), new{user = Model.GetType().GetProperty("Id").GetValue(Model,new object[0])},
            "~/images/icons/application_edit.png", "Edit the user") %>
<%}%>