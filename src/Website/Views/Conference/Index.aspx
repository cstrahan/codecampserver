<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="MvcContrib"%>
<%@ Import Namespace="System.Collections.Generic"%>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>
<%@ Import namespace="System.Web.Mvc"%>
<%@ Import namespace="Microsoft.Web.Mvc"%>

<asp:Content ContentPlaceHolderID="SidebarContentPlaceHolder" ID="SidebarContent" runat="server">
    <div>
        <% var conference = ViewData.Get<Schedule>(); %>
        <%=Html.ActionLink<ConferenceController>(c => c.PleaseRegister(conference.Key), "Register Now!") %><br />
        <%=Html.ActionLink<ScheduleController>(c => c.Index(conference.Key), "Schedule") %><br />
        <%=Html.ActionLink<ConferenceController>(c => c.ListAttendees(conference.Key), "List Attendees") %>
    </div>
</asp:Content>


<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" ID="MainContent" runat="server">
    <% var conference = ViewData.Get<Schedule>();%>
    Conference name: <%=conference.Name %> <br />

    <% if (conference.DaysUntilStart.HasValue) { %>
        There are <%=conference.DaysUntilStart.ToString()%> days remaining until the conference starts.<br />
    <% } %>


    Starts: <%=conference.StartDate.ToString()%>
</asp:Content>
