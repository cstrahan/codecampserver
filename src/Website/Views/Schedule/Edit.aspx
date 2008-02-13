?<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.Website.Views.ViewBase" Title="Edit Schedule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <% ScheduledConference conference = ViewData.Get<ScheduledConference>(); %>
    <form method="post" action='<%= Url.Action("Save") %>'>
        <fieldset>
            <legend>Edit the Schedule</legend>
            <% foreach (ScheduleListing listing in conference.GetSchedule())
               { %>
                Put the listing here!<br />
            <% } %>
        </fieldset>
    </form>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
