<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<UserGroupInput>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers.Extensions"%>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
<%= Html.EditImageButton(Url.Action("Edit", new { entityToEdit = Model.Id }), CodeCampSite.Admin.EditUserGroup)%>

<%}%>