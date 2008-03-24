<%@ Page Language="C#" AutoEventWireup="true" Title="Schedule" 
	Inherits="System.Web.Mvc.ViewPage" 
	MasterPageFile="~/Views/Layouts/Default.Master" %>

<%@ Import Namespace="CodeCampServer.Website.Views" %>
<%@ Import Namespace="CodeCampServer.Model.Presentation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" 
	runat="server">
	<div style="padding: 10px">
		<% Schedule conference = ViewData.Get<Schedule>(); %>
		<% ScheduleListing[] schedule = conference.GetScheduleListings(); %>
		<h1>
			<%= conference.Name %>
			Schedule</h1>
		<table cellpadding="2" cellspacing="0" width="100%">
			<tr class="header-row">
				<th>
					Start Time
				</th>
				<th>
					End Time
				</th>
				<th>
					Sessions
				</th>
			</tr>
			<% foreach (ScheduleListing listing in schedule)
	  { %>
			<tr>
				<td style="vertical-align: top">
					<%=listing.StartTime %>
				</td>
				<td style="vertical-align: top">
					<%=listing.EndTime %>
				</td>
				<td style="vertical-align: top">
					<% foreach (SessionListing session in listing.Sessions)
		{ %>
					<%= session.Title %>
					(<%= session.Listing.DisplayName %>)<br />
					<% } %>
				</td>
			</tr>
			<% } %>
		</table>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" 
	runat="server">
</asp:Content>
