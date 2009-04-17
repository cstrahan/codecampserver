<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<TrackForm[]>" %>
<%@ Import Namespace="MvcContrib.UI.Grid"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">
<h2>Tracks
		<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<TrackController>(c=>c.New(null))%>" title="Add a new Track"><img src="/images/icons/application_add.png" /></a>
		<%}%>
</h2>
<%=Errors.Display() %>
    <%=Html.Grid(Model)
	    .WithClass("datatable")
	    .AutoColumns()
	     .Columns(
	         builder =>{
	                       builder.For("Edit").
                           PartialCell("EditTrackLink").
                           Visible(User.Identity.IsAuthenticated);

                           builder.For("Delete").
                           PartialCell("DeleteTrackLink").
                           Visible(User.Identity.IsAuthenticated);
             })%>
</asp:Content>
