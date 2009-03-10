<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<UserGroupForm>" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
<a href="<%= Url.Action<UserGroupController>(t => t.Edit(null), new{usergroup = Model.Id}).ToXHTMLLink() %>"><img src="/images/icons/application_edit.png" /></a>
<%}%>