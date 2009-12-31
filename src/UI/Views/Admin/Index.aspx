<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="ViewPage<AdminInput>"%>


<asp:Content ContentPlaceHolderID="Main" runat="server">
	  <h2>Administration</h2>  
	  <ul class="square">
			<li><a href="<%=Url.Action<UserGroupController>(c=>c.List()) %>"> Edit User Groups</a></li>
			<li><a href="<%=Url.Action<UserController>(c=>c.Index()) %>"> Edit Users</a></li>
			<li><a href="<%=Url.Action<SponsorController>(c=>c.Index(null)) %>"> Edit Sponsors</a></li>
	  </ul>
</asp:Content>
