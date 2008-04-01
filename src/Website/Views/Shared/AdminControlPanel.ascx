<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>

<div class="admin-panel">
    <h3>Administration</h3>
    <ul>
        <li><%= Html.ActionLink<ConferenceController>(c=>c.List(), "Conferences") %></li>        
        <li><%= Html.ActionLink<AdminController>(c=>c.Index(), "Site Admins") %></li>
        
    </ul>
</div>
