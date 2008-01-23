<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" 
	Inherits="CodeCampServer.Website.Views.ViewBase" Title="CodeCampServer - New Session Successfully Created!" %>
<%@ Import namespace="CodeCampServer.Model.Domain"%>

<asp:Content ID="Header" ContentPlaceHolderID="head" runat="server">
    <link href="../content/css/forms.css" type="text/css" rel="Stylesheet" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <% Session _session = ViewData.Get<Session>(); %>
    
    Thank you for registering your session!
    <br />
    <table>
        <tr>
            <th>Speaker:</th>
            <td><%=_session.Speaker.Contact.FullName %></td>
        </tr>
        <tr>
            <th>Title:</th>
            <td><%=_session.Title%></td>
        </tr>
        <tr>
            <th>Abstract:</th>
            <td><%=_session.Abstract%></td>
        </tr>
        <% foreach(OnlineResource resource in _session.GetResources()) { %>
		<tr>
		    <th><%=resource.Type.ToString() %>:</th>
            <td><%=resource.Name %> (<a href='<%=resource.Href %>' target="_blank"><%=resource.Href %></a>)</td>
		</tr>
		<% } %>
    </table>
</asp:Content>