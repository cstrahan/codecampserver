<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowSchedule.aspx.cs" Inherits="CodeCampServer.Website.Views.Conference.ShowSchedule" MasterPageFile="~/Views/Layouts/TwoColumn.Master" %>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>

<asp:Content ContentPlaceHolderID="head" runat="server">
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
<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div class="schedule-listing">
		<h1><%=ViewData.Name %> Schedule</h1>
		
		<table cellpadding="2" cellspacing="0">
			<tr class="header-row">
				<th>Start Time</th>
				<th>End Time</th>
				<th>Title</th>
				<th>Speaker</th>
			</tr>
			<% foreach (ScheduleListing listing in ViewData.GetSchedule()) { %>
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

<script runat="server">

	protected override void OnPreInit(EventArgs e)
	{
		base.OnPreInit(e);
		Title = "Conference Schedule for " + ViewData.Name;//should this go in the controller
	} 
	
</script>
