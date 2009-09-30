<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<UserGroupInput>" %>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
<a href="<%= Url.Action<UserGroupController>(t => t.Edit(Guid.Empty), new{Id = Model.Id}).ToXHTMLLink() %>"><img src="/images/icons/application_edit.png" /></a>
<%}%>