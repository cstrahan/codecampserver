<%@ Page Language="C#" AutoEventWireup="true" 
	Title="Schedule" Inherits="CodeCampServer.Website.Views.ViewBase" MasterPageFile="~/Views/Layouts/Default.Master" %>
<%@ Import namespace="CodeCampServer.Website.Views"%>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    div.schedule-listing 
    {
    	margin: 10px;
    }
    
    div.schedule-listing h1
    {
    	text-align: center;
    }
    
    div.schedule-listing table
    {
    	width: 650px;
    }
    
    div.schedule-listing table tr.header-row
    {
    	background-color: #eee;
    	border-bottom: solid 1px #aaa;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div class="schedule-listing">
	    <% ScheduledConference conference = ViewData.Get<ScheduledConference>(); %>
	    <% ScheduleListing[] schedule = ViewData.Get<ScheduleListing[]>(); %>
		<h1><%= conference.Name %> Schedule</h1>
		
		<table cellpadding="2" cellspacing="0">
			<tr class="header-row">
				<th>Start Time</th>
				<th>End Time</th>
				<th>Sessions</th>
			</tr>
			<% foreach (ScheduleListing listing in schedule) { %>
			<tr>
				<td><%=listing.StartTime %></td>
				<td><%=listing.EndTime %></td>
				<td>
				    <% foreach (SessionListing session in listing.Sessions)
                       { %>
                       <%= session.Title %> (<%= session.Speaker.DisplayName %>)<br />
				    <% } %>
				</td>
			</tr>
			<% } %>
		</table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
