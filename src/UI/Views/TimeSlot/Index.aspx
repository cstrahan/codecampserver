<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI"%>

<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="MvcContrib"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
  <%var timeslots = (TimeSlotForm[])ViewData.Model; %>
  <div class="dataContainerQuadWide mt10">
	 <div class="cleaner"></div>

	<a class="" href="<%=Url.Action<TimeSlotController>(c=>c.New(null))%>" title="Add new Time Slot">Add</a>	 
	 <table class="genericBordered mt10 mb5">
		  <colgroup>
				<col />
				<col />
				<col />
		  </colgroup>
		  <tr>
				<th>Details</th>
				<th class="w30p tal"><strong>Name</strong></th>
				<th><strong>Conference Dates</strong></th>
		  </tr>
		  <% var counter = 0;
	   foreach (var timeslot in timeslots)
		{%>
		  <tr class="">
				<td><a class="" href="<%=Url.Action<TimeSlotController>(c=>c.Edit(null),new{timeslot=timeslot.Id})%>" title="View Time Slot <%= counter + 1 %>">Edit</a></td>				
				<td class="w30p tal"><strong><%= timeslot.StartTime%></strong></td>
				<td><%= timeslot.EndTime%> </td>
		  </tr>
		  <%
		counter++;
		 } 
		  %>
	 </table>
</div>
</asp:Content>