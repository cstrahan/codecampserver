<%@ Page Language="C#" Title="Conference Schedule for <%=ViewData.ConferenceName %>" AutoEventWireup="true" 
CodeBehind="ShowSchedule.aspx.cs" Inherits="CodeCampServer.Website.Views.Conference.ShowSchedule" MasterPageFile="~/Views/Layouts/Default.Master" %>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>
<asp:Content ContentPlaceHolderID="Head" runat="server">
	<title>Conference Schedule for <%=ViewData.ConferenceName %></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Center" runat="server">
    <h1><%=ViewData.ConferenceName %> Schedule</h1>
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
</asp:Content>
