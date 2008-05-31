<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>

<div class="admin-panel">
    <h3>Administration</h3>
    <ul>
        <%=Html.ActionLink<ConferenceController>(c=>c.List(), "Manage Conferences") %>
    </ul>
</div>
