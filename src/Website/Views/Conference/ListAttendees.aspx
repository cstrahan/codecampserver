<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" CodeBehind="ListAttendees.aspx.cs" Inherits="CodeCampServer.Website.Views.Conference.ListAttendees" Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<h3>Attendee List for <%= ViewData.Conference.Name %></h3>
<%
	attendeeGrid.DataSource = ViewData.Attendees;
	attendeeGrid.DataBind();
%>

<asp:DataGrid ID="attendeeGrid" runat="server" />

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
    <h2>Want to attend?</h2>
    <%= Html.ActionLink("Register Now!", "PleaseRegister") %>
</asp:Content>
