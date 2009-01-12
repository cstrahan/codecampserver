<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.SessionIndexView" %>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<%@ Import Namespace="CodeCampServer.UI"%>

<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
	<h2>Sessions
		<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<SessionController>(c=>c.New())%>" title="Add a new Session"><img src="/images/icons/application_add.png" /></a>
		<%}%>
	</h2>
	<% foreach (var session in ViewData.Model) { %>		
		<p class="sessionname"><a href="<%= Url.Action<SessionController>(t => t.Index(null), new{session = session.Id}).ToXHTMLLink() %>"><%= session.Title%></a>
		<%if (User.Identity.IsAuthenticated){%>
				<a href="<%= Url.Action<SessionController>(t => t.Edit(null), new{session = session.Id}).ToXHTMLLink() %>"><img src="/images/icons/application_edit.png" /></a>
		<%}%>		
		</p>
	<% } %>
	
</asp:Content>
