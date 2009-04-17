<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<UserGroupForm>"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
<script type="text/javascript" src="/scripts/rsswidget.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Menu" runat="server">
<% Html.RenderPartial("HomeMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
        <div class="w95p tac">
            <h2><%= Model.Name %> <%Html.RenderPartial("EditUserGroupLink", Model); %></h2>
            <p><%= Model.City %> <%= Model.Region %> <%= Model.Country%></p>             
        </div>
        <%Html.RenderPartial("ConferenceList", ViewData.Get<ConferenceForm[]>()); %>
        <div class="w95p">
        <%= Model.HomepageHTML %>
        </div>
        <%Html.RenderPartial("Sponsors", Model.Sponsors);%>
        <div class="cleaner"></div>
</asp:Content>