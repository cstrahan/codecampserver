<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<UserGroupForm>"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
<script type="text/javascript" src="/scripts/rsswidget.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Menu" runat="server">
<% Html.RenderPartial("HomeMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
        <div class="section-title">
            <h2><%= Model.Name %> <%Html.RenderPartial("EditUserGroupLink", Model); %></h2>
            <p><%= Model.City %> <%= Model.Region %> <%= Model.Country%></p>             
        </div>
        <div class="sidebar"><%Html.RenderPartial("Sponsors", Model.Sponsors);%></div>
        <div class="content"><%Html.RenderPartial("ConferenceList", ViewData.Get<ConferenceForm[]>()); %></div>
        
        <div class="cleaner"></div>
        <p></p>
        <%= Model.HomepageHTML %>        
        
        <div class="cleaner"></div>
</asp:Content>