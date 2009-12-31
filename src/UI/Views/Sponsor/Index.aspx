<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="ViewPage<SponsorInput[]>"%>
<%@ Import Namespace="MvcContrib.UI.Grid"%>
<%@ Import Namespace="CodeCampServer.Core.Domain.Model"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
	 <h2><%=ViewData.Get<PageInfo>().SubTitle%> Sponsors 
		<%if (User.Identity.IsAuthenticated){%>
			<a class="" href="<%=Url.Action<SponsorController>(c=>c.Edit(null,(Sponsor)null))%>" title="Add a new Sponsor"><img src="/images/icons/application_add.png" alt="Add a new Sponsor" /></a>
		<%}%>
	</h2>
	<%=Html.ValidationSummary() %>	
	<%=Html.Grid(Model)
	    .WithClass("datatable")
	    .AutoColumns()
	     .Columns(
	         builder =>{
	                       builder.For("Action").
                           PartialCell("EditSponsorLink").
                           Visible(User.Identity.IsAuthenticated);
                    })%>
</asp:Content>
