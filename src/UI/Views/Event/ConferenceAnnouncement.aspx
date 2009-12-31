<%@ Page Language="C#" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.BaseViewPage<ConferenceInput>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<h1 class="title"><%=Model.Name%> </h1> 
<div class="entry"> 

    <p class="display"><span class="display-label">When:</span> <%=Model.GetDate() %> </p> 
    <p class="display"><span class="display-label">Location:</span> <%=Model.LocationName %> - <a href="<%=Model.LocationUrl %>" class="more">map</a></p> 
    <p class="display"><span class="display-label">Conference info:</span> <%=Model.Description %></p> 
</div> 
