<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
    Inherits="System.Web.Mvc.ViewPage" Title="Speaker List" %>
<%@ Import namespace="CodeCampServer.Website.Views"%>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>
<%@ Import Namespace="CodeCampServer.Model.Domain" %>
<%@ Import Namespace="CodeCampServer.Model.Presentation" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Web.Mvc" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<h3>Speaker List for <%=ViewData.Get<Schedule>().Name%></h3>

<ul>
<% var conference = ViewData.Get<Schedule>(); %>
<% foreach (SpeakerListing speaker in ViewData.Get<SpeakerListingCollection>()) { %>
    <li>
        <span class="speaker-name">
        <%=Html.ActionLink<SpeakerController>(c=>c.View(conference.Key, speaker.Key), speaker.DisplayName) %>
        </span>        
    </li>
<% } %>
</ul>

</asp:Content>
