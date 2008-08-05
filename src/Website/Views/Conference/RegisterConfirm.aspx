<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage" Title="Registration confirmed" %>
<%@ Import namespace="MvcContrib"%>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<%
	Schedule conference = ViewData.Get<Schedule>();
	Person attendee = ViewData.Get<Person>();
%>
<h3>Thank you for registering for <%=conference.Name %>:</h3>
<br />
<%=attendee.GetName() %><br />
<%=attendee.Contact.Email %><br />
<%=attendee.Website %><br />
<%=attendee.Comment %><br />
</asp:Content>
