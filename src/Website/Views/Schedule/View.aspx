<%@ Page Language="C#" AutoEventWireup="true" Title="Schedule" 
	Inherits="System.Web.Mvc.ViewPage" 
	MasterPageFile="~/Views/Layouts/Default.Master" %>

<%@ Import Namespace="CodeCampServer.Website.Views" %>
<%@ Import Namespace="CodeCampServer.Model.Presentation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div style="padding:10px">
	    <% Schedule conference = ViewData.Get<Schedule>(); %>
	    <% ScheduleListing[] schedule = conference.GetScheduleListings(); %>
	    <% TrackListing[] tracks = conference.GetTrackListings(); %>
		<h1><%= conference.Name %> Schedule</h1>
			<table cellpadding="2" cellspacing="0" width="100%">
				<tr class="header-row">
				    <th></th>
				    <% foreach (TrackListing track in tracks)
                       { %>
                       <th><%= track.Name %></th>
                    <% } %>
				</tr>
				<% foreach (ScheduleListing listing in schedule) { %>
				<tr>
					<td style="vertical-align:top"><%=listing.StartTime %> - <%=listing.EndTime %><br /><%= listing.Purpose %></td>
					<% foreach (TrackListing track in tracks)
					{ %>
					<td style="vertical-align:top">
					   <% SessionListing session = listing[track];
                          if (session != null)
                          { %>
                           <%= session.Title%><br />(<%= session.Speaker.DisplayName%>)
                       <% }
                          else
                          { %>
                             <i>None</i>
                       <% } %>
					</td>
					<% } %>
				</tr>
				<% } %>
			</table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" 
	runat="server">
</asp:Content>
