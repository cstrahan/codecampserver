<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ConferenceForm[]>"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <%var conferences = Model; %>
  <div class="dataContainerQuadWide mt10">
	 <div class="cleaner"></div>
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
       foreach (var conference in conferences)
		{%>
		  <tr class="">
				
				
				<td class="w30p tal"><a href="<%=Url.RouteUrl("conferencedefault",new{conferencekey=conference.Key,action="index"}).ToXHTMLLink()%>"><strong><%= conference.Name%></strong></a></td>
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