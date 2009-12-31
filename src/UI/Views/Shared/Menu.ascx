<%@ Import Namespace="CodeCampServer.Core.Domain.Model"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CodeCampServer.Core.Domain.Model.Enumerations"%>

<% var conference = ViewData.Get<Conference>(); %>
    <ul class="menu"> 
		<asp:LoginView ID="LoginView1" runat="server">
			<LoggedInTemplate>
				<li><a href="<%=Url.Action<AdminController>(c=>c.Index(null)) %>"><b>admin</b></a></li>
			</LoggedInTemplate>
		</asp:LoginView>

		<li><a href="<%=Url.RouteUrl("conferencedefault",new{controller="conference",action="index",conferencekey=conference.Key}) %>"><b>home</b></a></li>
	</ul>
