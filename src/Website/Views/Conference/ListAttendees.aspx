<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" 
	Inherits="CodeCampServer.Website.Views.ViewBase" Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<h3>Attendee List for <%= ViewData.Get<ScheduledConference>().Name %></h3>
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
