<%@ Page Language="C#" Title="Conference Details" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>


<asp:Content ContentPlaceHolderID="Left" runat="server">
<div>
<%=Html.ActionLink("Register Now!", new{ action = "pleaseregister", conferenceKey = getConference().Key}) %>
<br />
<%=Html.ActionLink("Schedule", new{ action = "schedule", conferenceKey = getConference().Key}) %>
<br />
<%=Html.ActionLink("List Attendees", new{ action = "listattendees", conferenceKey = getConference().Key}) %>
</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Center" runat="server">
Conference name: <%=getConference().Name %>
<br />
<% if (getConference().DaysUntilStart.HasValue) { %>
There are <%=getConference().DaysUntilStart.ToString() %> days remaining until the conference starts.
<br />
<% } %>
Starts: <%=getConference().StartDate %>
</asp:Content>

<script runat="server">
	private ScheduledConference getConference()
	{
		ScheduledConference conference = (ScheduledConference)ViewData["conference"];
		return conference;
	}

</script>
