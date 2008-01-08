<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true"  
Inherits="System.Web.Mvc.ViewPage" Title="Attendee Listing" %>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>
<%@ Import namespace="System.Collections.Generic"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>
<asp:Content ContentPlaceHolderID="Left" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Center" runat="server">
<%
	ScheduledConference conference = (ScheduledConference)ViewData["conference"];
	IEnumerable<AttendeeListing> attendees = (IEnumerable<AttendeeListing>)ViewData["attendees"];
%>
<h3>Attendee List for <%=conference.Name %></h3>
<%
	attendeeGrid.DataSource = attendees;
	attendeeGrid.DataBind();
%>

<asp:DataGrid ID="attendeeGrid" runat="server" />

</asp:Content>
