<%@ Page Language="C#" AutoEventWireup="true" 
CodeBehind="ShowSchedule.aspx.cs" Inherits="CodeCampServer.Website.Views.Conference.ShowSchedule" MasterPageFile="~/Views/Layouts/Default.Master" %>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>

<asp:Content ContentPlaceHolderID="Center" runat="server">
	<div style="margin:10px;width:100%">
		<h1 style="text-align:center"><%=ViewData.ConferenceName %> Schedule</h1>
		<table style="width:650px" cellpadding="2" cellspacing="0">
			<tr style="background-color:#DDD">
				<th>Start Time</th>
				<th>End Time</th>
				<th>Title</th>
				<th>Speaker</th>
			</tr>
			<% foreach (ScheduleListing listing in ViewData.GetListings()) { %>
			<tr style="background-color:#EEE">
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
		Title = "Conference Schedule for " + ViewData.ConferenceName;//should this go in the controller
	} 
	
</script>