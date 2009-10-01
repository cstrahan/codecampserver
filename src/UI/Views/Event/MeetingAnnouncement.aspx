<%@ Page Language="C#" AutoEventWireup="true" Inherits="ViewPage<MeetingAnnouncementDisplay>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<h1 class="title"> <%=Model.Heading %> <%Html.RenderPartial("EditMeetingLink", Model); %> 
  <%Html.RenderPartial("DeleteMeetingLink", Model); %></h1> 
<div class="entry"> 
    <p><b>When:</b> <%=Model.When %>
      (<a href="<%=Model.LocationUrl %>"><%=Model.LocationName %></a>)</p>
    <p><b>Topic: <i><%=Model.Topic %></i></b><br />
      <%=Model.Summary.Replace("\r\n","<br/>") %></p> 
    <p><b>Speaker:</b> <a href="<%=Model.SpeakerUrl %>"><%=Model.SpeakerName %></a>
      - <%=Model.SpeakerBio.Replace("\r\n", "<br/>")%></p> 
    <p><b>Meeting info:</b> <%=Model.MeetingInfo %></p> 
</div> 