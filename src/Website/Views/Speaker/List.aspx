<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="CodeCampServer.Website.Views.Speaker.List" Title="Untitled Page" %>
<%@ Import Namespace="CodeCampServer.Model.Domain" %>
<%@ Import Namespace="CodeCampServer.Model.Presentation" %>
<%@ Import Namespace="System.Collections.Generic" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<h3>Speaker List for <%=ViewData.Conference.Name %></h3>

<ul>
<% foreach(SpeakerListing speaker in ViewData.Speakers) {%>
    <li>
        <span class="speaker-name"><%=Html.ActionLink(speaker.Name, new { action = "view", speakerId = speaker.DisplayName })%></span>        
    </li>
<% } %>
</ul>

</asp:Content>
