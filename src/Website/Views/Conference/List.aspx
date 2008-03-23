<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
    Inherits="System.Web.Mvc.ViewPage" Title="CodeCampServer - View Conferences" %>
<%@ Import namespace="CodeCampServer.Website.Views"%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <% foreach(var conf in ViewData.Get<Conference[]>()) { %>
    
    <ul>
        <li><%= conf.Name %></li>
    </ul>
    
    <% } %>

</asp:Content>
