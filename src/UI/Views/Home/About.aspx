<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
    Inherits="ViewPage<UserGroupInput>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <%= Model.HomepageHTML %>
</asp:Content>
