<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.TimeSlotIndexView"%>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI"%>

<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="MvcContrib"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
<h2>Timeslots
		<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<TimeSlotController>(c=>c.New(null))%>" title="Add a new Timeslot"><img src="/images/icons/application_add.png" /></a>
		<%}%>
</h2>
<p>
	<% foreach (var timeslot in ViewData.Model) { %>
		<div class=" w450 ">
			<div class="fl"><a href="<%= Url.Action<TimeSlotController>(t => t.Edit(null), new{track = timeslot.Id}).ToXHTMLLink() %>"><%= timeslot.StartTime %> to <%= timeslot.EndTime %></a></div>
			
			<div class="fr pr15"><a title="edit" href="<%= Url.Action<TimeSlotController>(t => t.Edit(null), new{timeslot = timeslot.Id}).ToXHTMLLink() %>"><img src="<%= Url.Content("~/images/icons/application_edit.png").ToXHTMLLink() %>" /></a></div>
		</div>
	<% } %>	
</p>

</asp:Content>