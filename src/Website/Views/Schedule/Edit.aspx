?<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" 
AutoEventWireup="true" 
Inherits="System.Web.Mvc.ViewPage" Title="Edit Schedule" %>
<%@ Import namespace="MvcContrib"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <% Schedule conference = ViewData.Get<Schedule>(); %>
    <form method="post" action='<%= Url.Action("Save") %>'>
        <fieldset>
            <legend>Edit the Schedule</legend>
            <% foreach (ScheduleListing listing in conference.GetScheduleListings())
               { %>
                Put the listing here!<br />
            <% } %>
        </fieldset>
    </form>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
