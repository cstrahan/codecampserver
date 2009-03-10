<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<UserGroupForm>"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
        <div class="w95p tac">
            <h2><%= Model.Name %></h2>
            <p><%= Model.City %><%= Model.Region %><%= Model.Country%></p>
             <p><%= Model.HomepageHTML %></p>
        </div>
</asp:Content>