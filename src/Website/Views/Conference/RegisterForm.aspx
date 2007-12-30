<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage" Title="Register for conference" %>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>
<%@ Import namespace="System.Web.Mvc"%>
<asp:Content ContentPlaceHolderID="Left" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Center" runat="server">
	<div style="text-align:center">
		<h3>Register for <%=getConference().Name %></h3>
		<%
			FormExtensions.SimpleForm form = Html.Form("register", "conference");
				 %>
		<table style="text-align:left">
			<tr>
				<td>First Name:</td>
				<td><%=Html.TextBox("firstName") %></td>
			</tr>
			<tr>
				<td>Last Name:</td>
				<td><%=Html.TextBox("lastName") %></td>
			</tr>
			<tr>
				<td>Email:</td>
				<td><%=Html.TextBox("email") %></td>
			</tr>
			<tr>
				<td>Website:</td>
				<td><%=Html.TextBox("website") %></td>
			</tr>
			<tr>
				<td>Comment:</td>
				<td><%=Html.TextArea("comment", "", 4, 40) %></td>
			</tr>
			<tr>
				<td colspan="2"><%=Html.SubmitButton("Register") %></td>
			</tr>
		</table>
		<%
			form.WriteEndTag(); %>
	</div>
</asp:Content>

<script runat="server">
	private Conference getConference()
	{
		Conference conference = (Conference) ViewData["conference"];
		return conference;
	}

</script>