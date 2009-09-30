<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<ConferenceInput>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<h1 class="title"><%=Model.Name%> <%Html.RenderPartial("EditConferenceLink", Model); %> <%Html.RenderPartial("DeleteConferenceLink", Model); %></h1> 
<div class="entry"> 
    <p><b>When:</b> <%=Model.GetDate() %> </p> 
    <p><b>Location:</b> <%=Model.LocationName %> - <a href="<%=Model.LocationUrl %>" class="more">map</a></p> 
    <p><b>Conference info:</b> <%=Model.Description %></p> 
</div> 
