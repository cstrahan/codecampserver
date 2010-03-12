<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="ViewPage<AdminInput>"%>


<asp:Content ContentPlaceHolderID="Main" runat="server">
	  <h2>Administration</h2>  
	  <ul class="square">
			<li><a href="<%=CodeCampServer.UI.Helpers.Extensions.UrlHelperExtensions.Action<UserGroupController>(Url, c=>c.List()) %>" rel="<%=CodeCampSite.Admin.EditUserGroups %>"> Edit User Groups</a></li>
			<li><a href="<%=CodeCampServer.UI.Helpers.Extensions.UrlHelperExtensions.Action<UserController>(Url, c=>c.Index()) %>"> Edit Users</a></li>
			<li><a href="<%=CodeCampServer.UI.Helpers.Extensions.UrlHelperExtensions.Action<SponsorController>(Url, c=>c.Index(null)) %>"> Edit Sponsors</a></li>
	  </ul>
</asp:Content>
