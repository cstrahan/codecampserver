<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<AdminForm>"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" runat="server">
<% Html.RenderPartial("HomeMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <div class="h100">
	  <h1>Administration</h1>  
	  <ul>
			<li><a href="<%=Url.Action<UserGroupController>(c=>c.List()) %>"> Edit User Groups</a></li>
			<li><a href="<%=Url.Action<UserController>(c=>c.Index()) %>"> Edit Users</a></li>
			<li><a href="<%=Url.Action<SponsorController>(c=>c.Index(null)) %>"> Edit Sponsors</a></li>
       <%if (Model.ConferenceIsSelected){%>
		    <li><a href="<%=Url.Action<ConferenceController>(c=>c.Edit(null)) %>"> Edit Conference</a></li>
		    <li><a href="<%=Url.Action<TrackController>(c=>c.Index(null) )%>"> Edit Tracks</a></li>
		    <li><a href="<%=Url.Action<TimeSlotController>(c=>c.Index(null)) %>"> Edit Timeslot</a></li>
		    <li><a href="<%=Url.Action<SpeakerController>(c=>c.List(null)) %>"> Edit Speakers</a></li>
		    <li><a href="<%=Url.Action<SessionController>(c=>c.List(null)) %>"> Edit Sessions</a></li>        			        
    <%} %>
	  </ul>
  </div>
</asp:Content>
