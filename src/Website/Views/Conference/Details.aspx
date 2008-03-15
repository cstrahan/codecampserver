<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
	Inherits="CodeCampServer.Website.Views.ViewBase" %>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>
<%@ Import namespace="System.Web.Mvc"%>

<asp:Content ContentPlaceHolderID="SidebarContentPlaceHolder" ID="SidebarContent" runat="server">

<div>
<%ScheduledConference conference = ViewData.Get<ScheduledConference>(); %>
<%=Html.ActionLink<ConferenceController>(c => c.PleaseRegister(conference.Key), "Register Now!") %>
<br />
<%=Html.ActionLink<ScheduleController>(c => c.Index(conference.Key), "Schedule") %>
<br />
<%=Html.ActionLink<ConferenceController>(c => c.ListAttendees(conference.Key,0,20), "List Attendees") %>
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
