<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" Title="Register for conference" %>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>
<%@ Import namespace="System.Web.Mvc"%>

<asp:Content ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div>
		<h3>Register for <%=getConference().Name %></h3>
		<% using (Html.Form("register", "conference")) { %>    
			
		<table>
			<tr>
				<td>First Name:</td>
				<td><%=Html.TextBox("firstName")%></td>
			</tr>
			<tr>
				<td>Last Name:</td>
				<td><%=Html.TextBox("lastName")%></td>
			</tr>
			<tr>
				<td>Email:</td>
				<td><%=Html.TextBox("email")%></td>
			</tr>
			<tr>
			    <td>Password:</td>
			    <td><%=Html.Password("password") %></td>
			</tr>
			<tr>
				<td>Website:</td>
				<td><%=Html.TextBox("website")%></td>
			</tr>
			<tr>
				<td>Comment:</td>
				<td><%=Html.TextArea("comment", "", 4, 40)%></td>
			</tr>
			<tr>
				<td colspan="2"><%=Html.SubmitButton("Register")%></td>
			</tr>
		</table>
		<% } %>
	</div>
</asp:Content>

<asp:Content ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">

</asp:Content>

<script runat="server">
	private ScheduledConference getConference()
	{
		ScheduledConference conference = (ScheduledConference)ViewData["conference"];
		return conference;
	}

</script>
