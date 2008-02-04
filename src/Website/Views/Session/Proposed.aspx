<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" 
    Inherits="CodeCampServer.Website.Views.ViewBase" Title="Proposed Sessions" %>
<%@ Import Namespace="CodeCampServer.Model.Domain" %>
<%@ Import Namespace="CodeCampServer.Model.Presentation" %>
<%@ Import Namespace="System.Collections.Generic" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h3>Proposed Sessions for <%=ViewData.Get<Conference>().Name%></h3>

    <ul>
    <% foreach (Session session in ViewData.Get<IEnumerable<Session>>())
       { %>
        <li>
            <span class="session-name"><%=session.Title%></span>        
        </li>
    <% } %>
    </ul>
</asp:Content>