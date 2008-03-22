<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
	Inherits="CodeCampServer.Website.Views.ViewBase" Title="Registration confirmed" %>
<%@ Import namespace="CodeCampServer.Website.Views"%>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<%
	ScheduledConference conference = ViewData.Get<ScheduledConference>();
	Person attendee = ViewData.Get<Person>();
%>
<h3>Thank you for registering for <%=conference.Name %>:</h3>
<br />
<%=attendee.GetName() %><br />
<%=attendee.Contact.Email %><br />
<%=attendee.Website %><br />
<%=attendee.Comment %><br />
</asp:Content>
