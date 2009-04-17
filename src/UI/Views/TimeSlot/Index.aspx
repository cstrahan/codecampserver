<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<TimeSlotForm[]>"%>
<%@ Import Namespace="MvcContrib.UI.Grid"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">
<h2>Timeslots
		<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<TimeSlotController>(c=>c.New(null))%>" title="Add a new Timeslot"><img src="/images/icons/application_add.png" /></a>
		<%}%>
</h2>
<%=Errors.Display() %>
<%=Html.Grid(Model)
	    .WithClass("datatable")
	    .AutoColumns()
	     .Columns(
	         builder =>{
	                       builder.For("Edit").
                           PartialCell("EditTimeSlotLink").
                           Visible(User.Identity.IsAuthenticated);

                           builder.For("Delete").
                           PartialCell("DeleteTimeSlotLink").
                           Visible(User.Identity.IsAuthenticated);
             })%>

</asp:Content>
