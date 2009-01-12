<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="MvcContrib"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
  <%var speakers = (SpeakerForm[])ViewData.Model; %>
  <div class="dataContainerQuadWide mt10">
	 <div class="cleaner"></div>
<a class="" href="<%=Url.Action<SpeakerController>(c=>c.New())%>" title="Add a new Speaker">Add</a>
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
				<th><strong></strong></th>
				<th class="w20p"><strong>Bio</strong></th>
		  </tr>
		  <% var counter = 0;
       foreach (var speaker in speakers)
		{%>
		  <tr class="">
				<td><a class="" href="<%=Url.Action<SpeakerController>(c=>c.Edit(null),new{speaker=speaker.Id})%>" title="View Speaker <%= counter + 1 %>">Edit</a></td>
				<td class="w30p tal"><strong><%= speaker.FirstName %> <%= speaker.LastName %></strong></td>
				<td></td>
				<td class="w20p"><%= speaker.Bio%><br />				                
				</td>
		  </tr>
		  <%
		counter++;
		 } 
		  %>
	 </table>
</div>
</asp:Content>