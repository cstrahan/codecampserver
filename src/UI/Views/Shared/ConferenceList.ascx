<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<ConferenceForm[]>" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>

 <table class="genericBordered mt10 mb5">
		  <colgroup>
				<col />
				<col />
				<col />
		  </colgroup>
		  <tr>
				<th class="w30p tal"><strong>Name</strong></th>
				<th><strong>Conference Dates</strong></th>
				<th class="w20p"><strong>Location</strong></th>
		  </tr>
		  <% var counter = 0;
       foreach (var conference in Model)
		{%>
		  <tr class="">
				<td class="w30p tal"><a href="<%=Url.RouteUrl("conferencedefault",new{conferencekey=conference.Key,controller="conference",action="index"}).ToXHTMLLink()%>"><strong><%= conference.Name%></strong></a></td>
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