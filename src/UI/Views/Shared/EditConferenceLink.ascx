<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<ConferenceForm>" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
<a href="<%= Url.Action<ConferenceController>(t => t.Edit(null), new{conference = Model.Key}).ToXHTMLLink() %>"><img src="/images/icons/application_edit.png" /></a>
<%}%>