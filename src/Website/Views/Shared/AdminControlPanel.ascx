<%@ Control Language="C#" AutoEventWireup="true" Inherits="CodeCampServer.Website.Views.ViewControlBase" %>

<div class="admin-panel">
    <h3>Administration</h3>
    <ul>
        <li><%= Html.ActionLink("Conferences", "list", "conference") %></li>
        <li><%= Html.ActionLink("Site Admins", "index", "admin") %></li>
        
    </ul>
</div>