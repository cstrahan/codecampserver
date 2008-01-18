<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="CodeCampServer.Website.Views.Speaker.View" Title="Untitled Page" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <%=ViewData.Contact.FullName%><br />
    <%=ViewData.Contact.Email%><br />
    <%=ViewData.Website%><br />
    <%=ViewData.Comment%><br />
    <%=ViewData.Profile%><br />
</asp:Content>
