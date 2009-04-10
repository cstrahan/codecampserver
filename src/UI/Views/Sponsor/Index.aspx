<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<SponsorForm[]>"%>
<%@ Import Namespace="MvcContrib.UI.Grid"%>
<%@ Import Namespace="CodeCampServer.Core.Domain.Model"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
		<script type="text/javascript" language="javascript" src="/scripts/jquery.dataTables.js"></script>
		<script type="text/javascript" charset="utf-8">
		    $(document).ready(function() {
		    $('#datatable').dataTable();
		    });
		</script>

	 <h2><%=ViewData.Get<PageInfo>().SubTitle%> / Sponsors 
		<%if (User.Identity.IsAuthenticated){%>
			<a class="" href="<%=Url.Action<SponsorController>(c=>c.New(null))%>" title="Add a new Sponsor"><img src="/images/icons/application_add.png" /></a>
		<%}%>
	</h2>
	<%=Errors.Display() %>	
	<%=Html.Grid(Model)
	    .WithID("datatable")
	    .AutoColumns()
	     .Columns(
	         builder =>{
	                       builder.For(
	                           x =>
	                           Html.ActionLink("Edit","Edit", new {sponsorID = x.ID})).DoNotEncode();
                    })%>
</asp:Content>
