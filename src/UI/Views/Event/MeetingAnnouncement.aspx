<%@ Page Language="C#" AutoEventWireup="true" 
  Inherits="ViewPage<MeetingAnnouncementDisplay>" %>

<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>
<h1 class="title">
  <%=Html.Display(m=>m.Heading).Partial(DisplayPartial.Inline).Label("") %>
  <%Html.RenderPartial("EditMeetingLink", Model); %>
  <%Html.RenderPartial("DeleteMeetingLink", Model); %></h1>
<div class="entry">
  <p>
    <%=Html.Display(m=>m.When).Partial(DisplayPartial.Inline) %>
    (<a href="<%=Model.LocationUrl %>"><%=Model.LocationName %></a>)
  </p>
  <%=Html.Display(m=>m.Topic) %>
  <%=Html.Display(m=>m.Summary).Label("") %>
  <p>
    <%=Html.Label(m=>m.SpeakerName)%>
    <a href="<%=Model.SpeakerUrl %>"><%=Model.SpeakerName %></a>
  </p>
  <%=Html.Display(m=>m.SpeakerBio).Label("") %>
  <%=Html.Display(m=>m.MeetingInfo) %>
</div>





<%----%>
<%--<h1 class="title">--%>
<%--  <%=Html.Display(m=>m.Heading)%>--%>
<%--</h1>--%>
<%--<div class="entry">--%>
<%--  <%=Html.Display(m=>m.When)%>--%>
<%--  <%=Html.Display(m=>m.LocationName)%>--%>
<%--  <%=Html.Display(m=>m.LocationUrl)%>--%>
<%--  <%=Html.Display(m=>m.Topic)%>--%>
<%--  <%=Html.Display(m=>m.Summary)%>--%>
<%--  <%=Html.Display(m=>m.SpeakerName)%>--%>
<%--  <%=Html.Display(m=>m.SpeakerUrl)%>--%>
<%--  <%=Html.Display(m=>m.SpeakerBio)%>--%>
<%--  <%=Html.Display(m=>m.MeetingInfo)%>--%>
<%--</div>--%>
