<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<UserGroupInput>" %>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
<a href="<%= Url.Action<UserGroupController>(t => t.Edit(Guid.Empty), new{entityToEdit = Model.Id}).ToXHTMLLink() %>"><img src="/images/icons/application_edit.png" alt="Edit User Group" /></a>
<%}%>