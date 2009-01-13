<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
var speaker = (SpeakerForm) ViewData.Model; %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
		<a href="<%= Url.Action<SpeakerController>(t => t.Edit(null), new{speaker = speaker.Id}).ToXHTMLLink() %>"><img src="/images/icons/application_edit.png" /></a>
<%}%>		
