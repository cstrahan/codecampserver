<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="ViewPage<SponsorInput>" %>

<%@ Import Namespace="CodeCampServer.Core.Common" %>
<%@ Import Namespace="MvcContrib.UI.InputBuilder" %>
<asp:Content ContentPlaceHolderID="Main" runat="server">
	<h3>
		Edit Sponsor</h3>
	<div class="onpage-form ui-dialog ui-widget ui-corner-all">
		<div class="ui-widget-content ui-corner-bottom">
			<div class="ui-dialog-content">
				<%=Html.InputForm() %>
				<%= Html.ActionLink<SponsorController>("Cancel", x=>x.Index(null)) %>
			</div>
		</div>
	</div>
</asp:Content>
