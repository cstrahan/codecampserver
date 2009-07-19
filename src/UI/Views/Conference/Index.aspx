<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ConferenceForm>"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
        <div class="section-title">
            <h2><%= Model.Name%> <%Html.RenderPartial("EditConferenceLink", Model); %></h2>
            <p><%= Model.Description %></p>
            <p><%= Model.LocationName %></p>            
            <p><%= Model.StartDate %> to <%= Model.EndDate %></p>
             <%= Model.HtmlContent %></p>
        </div>
</asp:Content>