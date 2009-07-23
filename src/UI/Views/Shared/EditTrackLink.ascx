<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<TrackForm>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>

<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
    <%= Html.ImageLink<TrackController>(c=>c.Edit(null), new{track=Model.Id}, 
        "~/images/icons/application_edit.png", "Edit " + Model.Name) %>    
<%}%>		
