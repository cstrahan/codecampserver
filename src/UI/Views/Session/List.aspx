<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.SessionIndexView" %>

<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>


<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
<h2>Sessions
		<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<SessionController>(c=>c.New())%>" title="Add a new Session"><img src="/images/icons/application_add.png" /></a>
		<%}%>
</h2>

	<% foreach (var session in ViewData.Model) { %>				
		<div class="w450 cb">
			<div class="fl"><a href="<%=Url.RouteUrl("session",new{sessionKey=session.Key}).ToXHTMLLink() %>" title="<%= session.TimeSlot.StartTime %> <%= session.Key %>">
				<%= session.Title%>
			</a></div>
			<div class="fr pl10"><%Html.RenderPartial(PartialViews.Shared.DeleteSessionLink,session); %></div>
			<div class="fr"><%Html.RenderPartial(PartialViews.Shared.EditSessionLink,session); %></div>
			<div class="cleaner"></div>
		</div>
	<% } %>
</asp:Content>
