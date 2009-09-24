<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
    Inherits="ViewPage<UserGroupForm>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <%= Model.HomepageHTML %>
</asp:Content>
