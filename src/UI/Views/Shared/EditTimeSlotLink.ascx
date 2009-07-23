<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TimeSlotForm>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
	
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
    <%= Html.ImageLink<TimeSlotController>(c=>c.Edit(null), new{timeslot=Model.Id},
            "~/images/icons/application_edit.png", "Edit the time slot" ) %>
<%}%>