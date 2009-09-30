<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<ConferenceInput[]>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>

<% if(Model.Length>0){ %>

 <table class="default">
    <caption>Meetings and Conferences</caption>
		  <colgroup>
				<col />
				<col />
				<col />
		  </colgroup>
		  <tr>
				<th>Name</th>
				<th>Event Dates</th>
				<th>Location</th>
		  </tr>
		  <% foreach (var conference in Model) {%>
		  <tr>
				<td><%= Html.ConferenceLink(conference) %></td>
				<td><%= conference.GetDate()%></td>
				<td><%= conference.LocationName%><br />
			        <%= conference.City%>, <%= conference.Region%> <%= conference.PostalCode%>
				</td>
		  </tr>
		  <% } %>
</table>
<%} %>