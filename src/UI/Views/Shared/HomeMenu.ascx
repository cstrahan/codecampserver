<%@ Control Language="C#" AutoEventWireup="true" 
Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="menu">
			<ul>
				<asp:LoginView ID="LoginView1" runat="server">
					<LoggedInTemplate>
						<li><a href="<%=Url.Action<AdminController>(c=>c.Index()) %>">Admin</a></li>
					</LoggedInTemplate>
				</asp:LoginView>
				
				<li><a href="<%=Url.Action<ConferenceController>(c=>c.List(null)) %>">Schedule</a></li>
			</ul>
</div>