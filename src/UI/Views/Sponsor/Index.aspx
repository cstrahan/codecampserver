<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="ViewPage<SponsorInput[]>"%>
<%@ Import Namespace="CodeCampServer.UI.Helpers.Extensions"%>
<%@ Import Namespace="MvcContrib.UI.Grid"%>
<%@ Import Namespace="CodeCampServer.Core.Domain.Model"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
	 <h2><%=ViewData.Get<PageInfo>().SubTitle%> Sponsors </h2>

			<form method="get" action="<%=(Url.Action("Edit", new {})) %>">
				<button class="ui-state-default ui-corner-all fg-button-icon-solo" type="submit" title="Add a new Sponsor">
						<span class="ui-icon ui-icon-plus"/>
				</button>
			</form>

	
	<%=Html.ValidationSummary() %>	
	<%=Html.Grid(Model)
	    .WithClass("fullWidth")
	    .AutoColumns()
	    .Columns(builder =>{ builder.For("Action").PartialCell("EditSponsorLink").Visible(User.Identity.IsAuthenticated); })%>
</asp:Content>