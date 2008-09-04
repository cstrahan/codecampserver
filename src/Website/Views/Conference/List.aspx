<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
    Inherits="System.Web.Mvc.ViewPage" Title="CodeCampServer - View Conferences" %>
<%@ Import Namespace="CodeCampServer.Website.Controllers"%>
<%@ Import namespace="MvcContrib" %>
<%@ Import namespace="Microsoft.Web.Mvc" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
  <script type="text/javascript">    
  </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    
    <ul>
        <li><%= Html.ActionLink<ConferenceController>(c=> c.New(), "Create a new conference") %></li>
    </ul>
    
    <% if(ViewData.Get<Conference[]>().Length > 0) { %> 
    <h2>Conferences:</h2>
    <table id="conferences" cellspacing="0">
        <tr>
        <th>Conference</th>
        <th>Starts</th>
        <th>Ends</th>
        <th>Displayed</th>
        <th></th>
        </tr>
        
        <% foreach(var conference in ViewData.Get<Conference[]>()) {%>
        
        <tr>
            <td><%= Html.ActionLink<ConferenceController>(c => c.Index(conference.Key), conference.Name) %></td>
            <td><%= conference.StartDate.Value.ToShortDateString() %></td>
            <td><%= conference.EndDate.HasValue ? 
                    conference.EndDate.Value.ToShortDateString() 
                    : conference.StartDate.Value.ToShortDateString() %></td>
            <td><%= Html.CheckBox("displayed", conference.PubliclyVisible, new{disabled="true"}) %></td>            
            <td><%= Html.ActionLink<ConferenceController>(c => c.Edit(conference.Key), "Edit") %></td>
            
        </tr>
        
        <% } %>        
    </table>
    <% } %>

</asp:Content>
