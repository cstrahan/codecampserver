<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="CodeCampServer.UI.ViewPage.BaseViewPage"%>
<%@ Import Namespace="CodeCampServer.UI.CSS"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <div class="h100">
  <h1>Administration</h1>  
  <ul>
      <li><a href="<%=Url.Action("editadminpassword") %>"> Update Admin Password</a></li>
      <li>Update Conference Information</li>
      <li>Update Conference Tracks</li>
      <li>Update Conference Timeslots</li>
      <li>Update Conference Sessions</li>
  </ul>
  </div>
  <%var conferences = (ConferenceForm[])ViewData.Model; %>
  <div class="dataContainerQuadWide mt10">
	 <div class="cleaner"></div>
	 <table class="genericBordered mt10 mb5">
		  <colgroup>
				<col />
				<col />
				<col />
				<col />
				<col />
				<col />
		  </colgroup>
		  <tr>
				<th>Details</th>
				<th><strong>Employment Dates</strong></th>
				<th class="w30p tal"><strong>Employer</strong></th>
				<th class="w20p"><strong>City/State</strong></th>
				<th>Primary</th>
				<th>Type</th>
		  </tr>
		  <% var counter = 0;
       foreach (var conference in conferences)
		{%>
		  <tr class="">
				<td><a class="" href="/conference/edit/<%=conference.Id%>" title="View Conference <%= counter + 1 %>">Edit</a></td>				
				<td><%= conference.StartDate%> To <%= conference.EndDate%></td>
				<td class="w30p tal"><strong><%= conference.Name%></strong></td>
				<td class="w20p"><%= conference.LocationName%></td>
				<td></td>
				<td></td>
		  </tr>
		  <%
		counter++;
		 } 
		  %>
	 </table>
</div>
</asp:Content>