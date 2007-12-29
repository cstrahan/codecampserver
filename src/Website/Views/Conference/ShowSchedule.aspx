<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowSchedule.aspx.cs" Inherits="CodeCampServer.Website.Views.Conference.ShowSchedule" %>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Conference Schedule for <%=ViewData.ConferenceName %></title>
</head>
<body>
    <div>
        <h1><%=ViewData.ConferenceName %> Schedule</h1>
        <table style="width:650px" cellpadding="2", cellspacing="0">
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
</body>
</html>
