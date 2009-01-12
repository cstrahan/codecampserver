<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<%@ Import Namespace="MvcContrib"%>
<%@ Import Namespace="CodeCampServer.Core.Domain.Model"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="menu">
			<ul>
				<asp:LoginView ID="LoginView1" runat="server">
					<LoggedInTemplate>
						<li><a href="<%=Url.Action<AdminController>(c=>c.Index()) %>">Admin</a></li>
					</LoggedInTemplate>
				</asp:LoginView>
				
				<li><a href="<%=Url.Action<SpeakerController>(c=>c.List()) %>">Speakers</a></li>
				<li><a href="<%=Url.Action<SessionController>(c=>c.List(null)) %>">Sessions</a></li>
				<li><a href="<%=Url.Action<AttendeeController>(c=>c.List(null)) %>">Attendees</a></li>
				<li><a href="<%=Url.Action<AttendeeController>(c=>c.New(null)) %>">Register</a></li>
			</ul>
		</div>