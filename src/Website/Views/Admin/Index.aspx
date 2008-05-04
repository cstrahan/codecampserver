<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Layouts/Admin.Master" %>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>
<%@ Import namespace="CodeCampServer.Website.Views"%>
<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    Administration
    
    <ul>
        <li><%= Html.ActionLink<ConferenceController>(c=> c.New(), "Create a new conference") %></li>
    </ul>
    
    <% if(ViewData.Get<Conference[]>().Length > 0) { %> 
    <h2>Conferences:</h2>
    <table cellspacing="0">
        <tr>
        <th>Conference</th>
        <th>Starts</th>
        <th>Ends</th>
        <th></th>
        <th></th>
        </tr>
        
        <% foreach(var conference in ViewData.Get<Conference[]>()) {%>
        
        <tr>
            <td><%= conference.Name %></td>
            <td><%= conference.StartDate.Value.ToShortDateString() %></td>
            <td><%= conference.EndDate.HasValue ? 
                    conference.EndDate.Value.ToShortDateString() 
                    : conference.StartDate.Value.ToShortDateString() %></td>
            <td><%= Html.ActionLink<ConferenceController>(c => c.Details(conference.Key), "View") %></td>
            <td><%= Html.ActionLink<ConferenceController>(c => c.Edit(conference.Key), "Edit") %></td>
        </tr>
        
        <% } %>        
    </table>
    <% } %>

</asp:Content>