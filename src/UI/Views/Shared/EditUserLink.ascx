<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers.Extensions"%>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.Core.Domain.Bases" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
<a href="<%=Url.Action<UserController>(t=>t.Edit((User)null), new{user = Model.GetType().GetProperty("Id").GetValue(Model,new object[0])}) %>"><img src="/images/icons/application_edit.png" alt="Edit User" /></a>
<%}%>