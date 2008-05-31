<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Layouts/Admin.Master" %>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

  <h1>Code Camp Server Administration</h1>

   <ul>
    <li><h3><%= Html.ActionLink<ConferenceController>(c=>c.List(), "Manage Conferences") %></h3></li>    
   </ul>

</asp:Content>