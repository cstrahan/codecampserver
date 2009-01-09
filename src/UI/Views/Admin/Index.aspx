<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="System.Security.Policy"%>
<%@ Import Namespace="MvcContrib" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
<% Html.RenderPartial("AdminMenu"); %>
</asp:Content>