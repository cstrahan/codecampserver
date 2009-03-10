<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<UserGroupForm[]>"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  
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
       foreach (var userGroup in Model)
		{%>
		  <tr class="">
				<td><a class="" href="<%=Url.Action<UserGroupController>(c=>c.Edit(null),new{usergroup=userGroup.Id})%>" title="View User Group <%= counter + 1 %>">Edit</a></td>				
				<td class="w30p tal"><strong><%= userGroup.Name%></strong></td>
				<td><%= userGroup.City%> <%= userGroup.Region%>,<%= userGroup.Country%> </td>
				<td class="w20p">
				                <%= userGroup.City%>, <%= userGroup.Region%> <%= userGroup.Country%>
				</td>
		  </tr>
		  <%
		counter++;
		 } 
		  %>
	 </table>
</div>
</asp:Content>