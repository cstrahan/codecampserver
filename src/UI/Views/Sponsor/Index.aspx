<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="ViewPage<SponsorInput[]>"%>
<%@ Import Namespace="CodeCampServer.UI.Helpers.Extensions"%>
<%@ Import Namespace="MvcContrib.UI.Grid"%>
<%@ Import Namespace="CodeCampServer.Core.Domain.Model"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
	 <h2><%=ViewData.Get<PageInfo>().SubTitle%> Sponsors 

		<%= Html.AddImageButton(Url.Action("Edit", new {}))%>

	</h2>
	<%=Html.ValidationSummary() %>	
	<%=Html.Grid(Model)
	    .WithClass("datatable")
	    .AutoColumns()
	    .Columns(builder =>{ builder.For("Action").PartialCell("EditSponsorLink").Visible(User.Identity.IsAuthenticated); })%>
</asp:Content>