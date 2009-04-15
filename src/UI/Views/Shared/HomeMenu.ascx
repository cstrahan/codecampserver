<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="navigationWrapper">
	<div id="leftNavigationEndCap"></div>
	<ul id="navigationMenu">
		<asp:LoginView ID="LoginView1" runat="server">
			<LoggedInTemplate>
				<li><a href="<%=Url.Action<AdminController>(c=>c.Index(null)) %>"><img src="~/images/themes/green/buttons/admin.gif" runat="server" id="admin" alt="Admin" /></a></li>
			</LoggedInTemplate>
		</asp:LoginView>
		
		<li><a href="<%=Url.Action<ConferenceController>(c=>c.List(null)) %>"><img src="~/images/themes/green/buttons/schedule.gif" runat="server" id="schedule" alt="Schedule" /></a></li>
		<li><a href="<%=Url.Action<UserGroupController>(c=>c.List()) %>"><img src="~/images/themes/green/buttons/user-groups.gif" runat="server" id="userGroups" alt="User Groups" /></a></li>
	</ul>
	<div id="rightNavigationEndCap"></div>
</div>
<div class="cleaner"></div>