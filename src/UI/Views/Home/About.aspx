<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
    Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<UserGroupForm>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <%= Model.HomepageHTML %>
</asp:Content>
