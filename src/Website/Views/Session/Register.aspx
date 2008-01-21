<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" 
	Inherits="CodeCampServer.Website.Views.ViewBase" Title="CodeCampServer - New Session" %>
    
<asp:Content ID="Header" ContentPlaceHolderID="head" runat="server">
    <link href="../content/css/forms.css" type="text/css" rel="Stylesheet" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div>
		<% using (Html.Form("create", "session")) { %>
		<table>
		    <tr>
		        <th>Speaker:</th>
		        <td><%=Html.Select("speakerKey", ViewData.Get<SpeakerListingCollection>(), "Name", "Key")%></td>
		    </tr>
			<tr>
				<th>Title:</th>
				<td><%=Html.TextBox("title", 80)%></td>
			</tr>
			<tr>
				<th>Abstract:</th>
				<td><%=Html.TextArea("abstract", "", 5, 80)%></td>
			</tr>
			<tr>
			    <th>Blog Name:</th>
				<td><%=Html.TextBox("blogName", 80)%></td>
			</tr>
			<tr>
			    <th>Blog Url:</th>
				<td><%=Html.TextBox("blogUrl", 80)%></td>
			</tr>
			<tr>
			    <th>Website Name:</th>
				<td><%=Html.TextBox("websiteName", 80)%></td>
			</tr>
			<tr>
			    <th>Website Url:</th>
				<td><%=Html.TextBox("websiteUrl", 80)%></td>
			</tr>
			<tr>
			    <th>Session Download Name:</th>
				<td><%=Html.TextBox("downloadName", 80)%></td>
			</tr>
			<tr>
			    <th>Session Download Url:</th>
				<td><%=Html.TextBox("downloadUrl", 80)%></td>
			</tr>
			<tr>
			    <td></td>
				<td><%=Html.SubmitButton("Register", "Register Session")%></td>
			</tr>
		</table>
		<% } %>
    </div>
</asp:Content>