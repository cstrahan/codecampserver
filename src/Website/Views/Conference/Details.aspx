<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="CodeCampServer.Website.Views.Conference.Details" %>
<%@ Import namespace="CodeCampServer.Model.Domain"%>

<asp:Content ContentPlaceHolderID="SidebarContentPlaceHolder" ID="SidebarContent" runat="server">

<div>
<%=Html.ActionLink("Register Now!", new{ action = "pleaseregister", conferenceKey = ViewData.Key}) %>
<br />
<%=Html.ActionLink("Schedule", new{ action = "schedule", conferenceKey = ViewData.Key}) %>
<br />
<%=Html.ActionLink("List Attendees", new{ action = "listattendees", conferenceKey = ViewData.Key}) %>
</div>
</asp:Content>


<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" ID="MainContent" runat="server">
Conference name: <%=ViewData.Name %>
<br />

<% if (ViewData.DaysUntilStart.HasValue) { %>
There are <%=ViewData.DaysUntilStart.ToString() %> days remaining until the conference starts.
<br />
<% } %>


Starts: <%=ViewData.StartDate.GetValueOrDefault().ToString() %>
</asp:Content>
