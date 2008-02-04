<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
    Inherits="CodeCampServer.Website.Views.ViewBase" Title="Speaker List" %>
<%@ Import Namespace="CodeCampServer.Model.Domain" %>
<%@ Import Namespace="CodeCampServer.Model.Presentation" %>
<%@ Import Namespace="System.Collections.Generic" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<h3>Speaker List for <%=ViewData.Get<ScheduledConference>().Name%></h3>

<ul>
<% foreach (SpeakerListing speaker in ViewData.Get<SpeakerListingCollection>()) { %>
    <li>
        <span class="speaker-name">
        <%=Html.ActionLink(speaker.Name, new { action = "view", speakerId = speaker.DisplayName })%>
        </span>        
    </li>
<% } %>
</ul>

</asp:Content>
