<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" Inherits="CodeCampServer.Website.Views.ViewBase" Title="CodeCampServer - View Conferences" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <% foreach(var conf in ViewData.Get<Conference[]>()) { %>
    
    <ul>
        <li><%= conf.Name %></li>
    </ul>
    
    <% } %>

</asp:Content>
