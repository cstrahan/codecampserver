<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
	Inherits="CodeCampServer.Website.Views.ViewBase" %>
<%@ Import namespace="CodeCampServer.Model.Domain"%>

<asp:Content ContentPlaceHolderID="SidebarContentPlaceHolder" ID="SidebarContent" runat="server">

<div>
<%ScheduledConference conference = ViewData.Get<ScheduledConference>(); %>
<%=Html.ActionLink("Register Now!", new{ action = "pleaseregister", conferenceKey = conference.Key}) %>
<br />
<%=Html.ActionLink("Schedule", new { Controller = "schedule", Action = "index", conferenceKey = conference.Key })%>
<br />
<%=Html.ActionLink("List Attendees", new{ action = "listattendees", conferenceKey = conference.Key}) %>
</div>
</asp:Content>


<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" ID="MainContent" runat="server">
<%ScheduledConference conference = ViewData.Get<ScheduledConference>();%>
Conference name: <%=conference.Name %>
<br />

<% if (conference.DaysUntilStart.HasValue) { %>
There are <%=conference.DaysUntilStart.ToString()%> days remaining until the conference starts.
<br />
<% } %>


Starts: <%=conference.StartDate.ToString()%>
</asp:Content>
