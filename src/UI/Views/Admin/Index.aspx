<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="System.Security.Policy"%>
<%@ Import Namespace="MvcContrib" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <div class="h100">
	  <h1>Administration</h1>  
	  <ul>
			<li><a href="<%=Url.Action<AdminController>(c=>c.Edit(null)) %>"> Edit Admin Record</a></li>
			<li><a href="<%=Url.Action<ConferenceController>(c=>c.Edit(null)) %>"> Edit Conference</a></li>
			<li><a href="<%=Url.Action<TrackController>(c=>c.Index(null) )%>"> Edit Tracks</a></li>
			<li><a href="<%=Url.Action<TimeSlotController>(c=>c.Index(null)) %>"> Edit Timeslot</a></li>
			<li><a href="<%=Url.Action<SpeakerController>(c=>c.List()) %>"> Edit Speakers</a></li>
			<li><a href="<%=Url.Action<SessionController>(c=>c.List(null)) %>"> Edit Sessions</a></li>        
	  </ul>
  </div>
</asp:Content>
