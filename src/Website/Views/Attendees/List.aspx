<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage" Title="Untitled Page" %>
<%@ Import namespace="CodeCampServer.Website.Views"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<h3>Attendee List for <%= ViewData.Get<Schedule>().Name %></h3>
<%
	attendeeGrid.DataSource = ViewData.Get<AttendeeListing[]>();
	attendeeGrid.DataBind();
%>

<asp:DataGrid ID="attendeeGrid" runat="server" />

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
    <h2>Want to attend?</h2>
    <%= Html.ActionLink("Register Now!", "PleaseRegister") %>
</asp:Content>
