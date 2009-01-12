<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="MvcContrib"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
  <%var conferences = (ConferenceForm[])ViewData.Model; %>
  <div class="dataContainerQuadWide mt10">
	 <div class="cleaner"></div>
	 <table class="genericBordered mt10 mb5">
		  <colgroup>
				<col />
				<col />
				<col />
				<col />
		  </colgroup>
		  <tr>
				<th>Details</th>
				<th class="w30p tal"><strong>Name</strong></th>
				<th><strong>Conference Dates</strong></th>
				<th class="w20p"><strong>Location</strong></th>
		  </tr>
		  <% var counter = 0;
       foreach (var conference in conferences)
		{%>
		  <tr class="">
				<td><a class="" href="<%=Url.Action<ConferenceController>(c=>c.Edit(null),new{conferenceKey=conference.Key})%>" title="View Conference <%= counter + 1 %>">Edit</a></td>				
				<td class="w30p tal"><strong><%= conference.Name%></strong></td>
				<td><%= conference.StartDate%> To <%= conference.EndDate%></td>
				<td class="w20p"><%= conference.LocationName%><br />
				                <%= conference.City%>, <%= conference.Region%> <%= conference.PostalCode%>
				</td>
		  </tr>
		  <%
		counter++;
		 } 
		  %>
	 </table>
</div>
</asp:Content>