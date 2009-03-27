<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ConferenceForm>"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
        <div class="w95p tac">
            <h2><%= Model.Description %> <%Html.RenderPartial("EditConferenceLink", Model); %></h2>
            <p><%= Model.LocationName %></p>
            <p><%= Model.StartDate %>&nbsp;to&nbsp;<%= Model.EndDate %></p>
             <%= Model.HtmlContent %></p>
        </div>
</asp:Content>