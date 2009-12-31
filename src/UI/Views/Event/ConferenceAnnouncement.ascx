<%@ Control Language="C#" AutoEventWireup="true" Inherits="ViewUserControl<ConferenceInput>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<h1 class="title"><%=Model.Name%> </h1> 
<div class="entry"> 
    <p><b>When:</b> <%=Model.GetDate() %> </p> 
    <p><b>Location:</b> <%=Model.LocationName %> - <a href="<%=Model.LocationUrl %>" class="more">map</a></p> 
    <p><b>Conference info:</b> <%=Model.Description %></p> 
</div> 
