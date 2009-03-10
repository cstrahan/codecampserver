<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<TimeSlotForm[]>"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">
<h2>Timeslots
		<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<TimeSlotController>(c=>c.New(null))%>" title="Add a new Timeslot"><img src="/images/icons/application_add.png" /></a>
		<%}%>
</h2>
<%=Errors.Display() %>
<p>
	<% foreach (var timeslot in ViewData.Model) { %>
		<div class=" w450 ">
			<div class="fl"><%= timeslot.StartTime %> to <%= timeslot.EndTime %></div>
			
			<div class="fr pr15">
			<%Html.RenderPartial("DeleteTimeSlotLink",timeslot); %>			
			</div>
			<div class="fr pr15">
			<%Html.RenderPartial("EditTimeSlotLink",timeslot); %>			
			</div>
			<div class="cleaner"></div>
		</div>
	<% } %>	
</p>

</asp:Content>
