<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Layouts/Admin.Master" %>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>
<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    Admin....
    
    <ul>
        <li><%= Html.ActionLink<ConferenceController>(c=> c.New(), "Create a new conference") %></li>
    </ul>

</asp:Content>