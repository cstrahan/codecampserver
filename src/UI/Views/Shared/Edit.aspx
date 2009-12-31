<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="ViewPage<object>"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
<h2>Edit <%=Html.GetLabelForModel() %></h2>
  <%=Html.InputFormForModel( ) %>
</asp:Content>