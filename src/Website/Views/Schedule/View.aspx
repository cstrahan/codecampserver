<%@ Page Language="C#" AutoEventWireup="true" 
	Title="Schedule" Inherits="CodeCampServer.Website.Views.ViewBase" MasterPageFile="~/Views/Layouts/TwoColumn.Master" %>
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
		<h1><%= conference.Name %> Schedule - sweet!</h1>
		
		<table cellpadding="2" cellspacing="0">
			<tr class="header-row">
				<th>Start Time</th>
				<th>End Time</th>
				<th>Title</th>
				<th>Speaker</th>
			</tr>
			<% foreach (ScheduleListing listing in conference.GetSchedule()) { %>
			<tr>
				<td><%=listing.StartTime %></td>
				<td><%=listing.EndTime %></td>
				<td><%=listing.SessionTitle %></td>
				<td><%=listing.SpeakerName %></td>
			</tr>
			<% } %>
		</table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
