<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
	Inherits="CodeCampServer.Website.Views.ViewBase" Title="CodeCampServer - New Session Successfully Created!" %>
<%@ Import namespace="CodeCampServer.Website.Views"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>

<asp:Content ID="Header" ContentPlaceHolderID="head" runat="server">
    <link href="../content/css/forms.css" type="text/css" rel="Stylesheet" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <% Session _session = ViewData.Get<Session>(); %>
    
    <fieldset>
        <legend>New Session Information</legend>
    
        <label for="speaker">Speaker</label>
        <%=_session.Speaker.Contact.FullName %>
        
        <label for="title">Title</label>
        <%=_session.Title%>
        
        <label for="abstract">Abstract</label>
        <%=_session.Abstract%>

    <% foreach(OnlineResource resource in _session.GetResources()) { %>
        <label><%=resource.Type.ToString() %></label>
        <p><%=resource.Name %> (<a href='<%=resource.Href %>' target="_blank"><%=resource.Href %></a>)</p>
	<% } %>
    </fieldset>
    <br />
    
    Thank you for registering!
</asp:Content>