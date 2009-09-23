<%@ Import Namespace="CodeCampServer.Core.Domain.Model"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CodeCampServer.Core.Domain.Model.Enumerations"%>

<% var conference = ViewData.Get<Conference>(); %>
<div id="menu"> 
    <ul> 
		<asp:LoginView ID="LoginView1" runat="server">
			<LoggedInTemplate>
				<li><a href="<%=Url.Action<AdminController>(c=>c.Index(null)) %>">admin</a></li>
			</LoggedInTemplate>
		</asp:LoginView>

		<li><a href="<%=Url.RouteUrl("conferencedefault",new{controller="conference",action="index",conferencekey=conference.Key}) %>">home</a></li>
	</ul>
</div>
