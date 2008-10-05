<%@ Page Language="C#" AutoEventWireup="true" Title="Schedule" Inherits="System.Web.Mvc.ViewPage"
    MasterPageFile="~/Views/Layouts/Default.Master" %>

<%@ Import Namespace="CodeCampServer.Website.Controllers" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="CodeCampServer.Model.Presentation" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $('#StartTime').timePicker();
            $('#EndTime').timePicker();

            $("form#add_session").validate({
                errorContainer: '#errors',
                errorClass: "formError",
                messages: {
                    Purpose: "Required",
                    StartTime: "Required",
                    EndTime: "Required"
                }
            });

            $("form#add_track").validate({
                errorContainer: '#errors',
                errorClass: "formError",
                messages: {
                    Description: "Required"
                }
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div style="padding: 10px">
        <% Schedule conference = ViewData.Get<Schedule>(); %>
        <% ScheduleListing[] schedule = conference.GetScheduleListings(); %>
        <% TrackListing[] tracks = conference.GetTrackListings(); %>
        <h1>
            <%= conference.Name %>
            Schedule</h1>
        <table>
            <tr class="header-row">
                <th>
                </th>
                <% foreach (TrackListing track in tracks)
                   { %>
                <th>
                    <%= track.Name %>
                </th>
                <% } %>
            </tr>
            <% foreach (ScheduleListing listing in schedule)
               { %>
            <tr>
                <td>
                    <%=listing.StartTime %>
                    -
                    <%=listing.EndTime %><br />
                    <%= listing.Purpose %>
                </td>
                <% foreach (TrackListing track in tracks)
                   { %>
                <td>
                    <% 
                        var trackId = track.Id;
                        var listingId = listing.Id; 
                    %>
                    <%= Html.ActionLink<ScheduleController>(c => c.Edit(conference.Key, trackId, listingId), "Edit")%>
                    <% SessionListing session = listing[track];
                       if (session != null)
                       { %>
                    <%= session.Title%><br />
                    (<%= session.Speaker.DisplayName%>)
                    <% }
                else
                { %>
                    <i>None</i>
                    <% } %>
                </td>
                <% } %>
            </tr>
            <% } %>
        </table>
    </div>
    
    <div style="padding: 10px">
        <h1>
            Add Session</h1>
        <form method="post" id="add_session" action='<%= Url.Action("AddTimeSlot") %>'>
        <fieldset style="float: none">
            <label for="purpose">
                Purpose</label>
            <%=Html.TextBox("purpose", new { @class = "required", size = "25", maxLength = "100" })%>
            <label for="StartTime">
                Start Time</label>
            <%=Html.TextBox("StartTime", new { @class = "required", size = "10", maxLength = "10" })%>
            <label for="EndTime">
                End Time</label>
            <%=Html.TextBox("EndTime", new { @class = "required", size = "10", maxLength = "10" })%>
            <%=Html.Hidden("conferenceKey", conference.Key)%>
            <div class="button-row">
                <%=Html.SubmitButton("Add", "Add")%>
            </div>
        </fieldset>
        </form>
    </div>
    
    <div style="padding: 10px">
        <h1>
            Add Track</h1>
        <form method="post" id="add_track" action='<%= Url.Action("AddTrack") %>'>
        <fieldset style="float: none">
            <label for="description">
                Description</label>
            <%=Html.TextBox("description", new { @class = "required", size = "25", maxLength = "100" })%>
            <%=Html.Hidden("conferenceKey", conference.Key)%>
            <div class="button-row">
                <%=Html.SubmitButton("Add", "Add")%>
            </div>
        </fieldset>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
