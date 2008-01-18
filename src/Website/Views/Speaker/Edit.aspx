<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="CodeCampServer.Website.Views.Speaker.Edit" Title="Untitled Page" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <div>
		<h3>Edit Profile</h3>
		<% var speaker = this.ViewData; %>
		<% using (Html.Form("save", "speaker")) { %>    
			
		<table>
		    <tr>
		        <td>Display Name:</td>
		        <td><%=Html.TextBox("displayName",speaker.DisplayName)%></td>
		    </tr>
			<tr>
				<td>First Name:</td>
				<td><%=Html.TextBox("firstName",speaker.Contact.FirstName)%></td>
			</tr>
			<tr>
				<td>Last Name:</td>
				<td><%=Html.TextBox("lastName", speaker.Contact.LastName)%></td>
			</tr>
			<tr>
				<td>Website:</td>
				<td><%=Html.TextBox("website", speaker.Website)%></td>
			</tr>
			<tr>
				<td>Comment:</td>
				<td><%=Html.TextArea("comment", speaker.Comment, 4, 40)%></td>
			</tr>
			<tr>
			    <td>Profile:</td>
			    <td><%=Html.TextArea("profile", speaker.Profile, 4, 40)%></td>
			</tr>
			<tr>
			    <td>Avatar Url:</td>
			    <td><%=Html.TextBox("avatarUrl", speaker.AvatarUrl)%></td>
			</tr>
			<tr>
				<td colspan="2"><%=Html.SubmitButton("Save")%></td>
			</tr>
		</table>
		<% } %>
	</div>

</asp:Content>
