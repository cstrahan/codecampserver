<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" Title="Code Camp Server - List of Conferences" %>
<%@ Import Namespace="MvcContrib"%>
<%@ Import Namespace="System.Collections.Generic"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<h3>Attendee List for <%= ViewData.Get<Schedule>().Name %></h3>

<ul>
<% foreach (var person in ViewData.Get<Person[]>()) { %>
    <li>
        <span class="attendee-name">
            <%= person.Contact.FullName %>
        </span>        
    </li>
<% } %>
</ul>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
    <h2>Want to attend?</h2>
    <%= Html.ActionLink("Register Now!", "PleaseRegister") %>
</asp:Content>
