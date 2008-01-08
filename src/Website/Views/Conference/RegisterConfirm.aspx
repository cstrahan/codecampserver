<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
Inherits="System.Web.Mvc.ViewPage" Title="Registration confirmed" %>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>
<asp:Content ID="Content2" ContentPlaceHolderID="Left" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Center" runat="server">
<%
	ScheduledConference conference = (ScheduledConference)ViewData["conference"];
	Attendee attendee = (Attendee) ViewData["attendee"];
%>
<h3>Thank you for registering for <%=conference.Name %>:</h3>
<br />
<%=attendee.GetName() %><br />
<%=attendee.Contact.Email %><br />
<%=attendee.Website %><br />
<%=attendee.Comment %><br />
</asp:Content>
