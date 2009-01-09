<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.TrackIndex" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
	<% Html.RenderPartial("AdminMenu"); %>
	<%var tracks = ViewData.Model; %>
	<div class="dataContainerQuadWide mt10">
		<div class="cleaner">
		</div>
		<table class="genericBordered mt10 mb5">
			<colgroup>
				<col />
			</colgroup>
			<tr>
				<th class="w30p tal">
					<strong>Name</strong>
				</th>
			</tr>
			<% var counter = 0;
		foreach (var track in tracks)
		{%>
			<tr class="">
				<%= track.Name %>
			</tr>
			<%
				counter++;
		} 
			%>
		</table>
	</div>
</asp:Content>
