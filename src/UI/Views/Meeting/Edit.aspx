<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	ValidateRequest="false" Inherits="ViewPage<MeetingInput>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
	<h3>Edit Meeting</h3>
	<div class="onpage-form ui-dialog ui-widget ui-corner-all">
		<div class="ui-widget-content ui-corner-bottom">
			<div class="ui-dialog-content">
				<%=Html.InputForm() %>
				<%= Html.ActionLink<HomeController>("Cancel", x=>x.Index(null)) %>
			</div>
		</div>
	</div>
</asp:Content>
