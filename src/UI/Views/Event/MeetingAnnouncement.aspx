<%@ Page Language="C#" AutoEventWireup="true" 
  Inherits="CodeCampServer.UI.Helpers.BaseViewPage<MeetingAnnouncementDisplay>" %>

<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<style>

.info
{
	margin-left: 173px;
}

</style>

<h1 class="title">
	<%= Model.Heading %>
	<%Html.RenderPartial("EditMeetingLink", Model); %>
	<%Html.RenderPartial("DeleteMeetingLink", Model); %>
</h1>

<div class="entry">

<p class="display"><span class="display-label">When:</span> <%=Model.When %></p>
<p class="display info">(<a href="<%=Model.LocationUrl %>"><%=Model.LocationName %></a>)</p>
<p class="display"><span class="display-label">Topic:</span> <%=Model.Topic %></p>
<p class="display info"><%=Model.Summary%></p>
 <p class="display"><span class="display-label">Speaker:</span> <a href="<%=Model.SpeakerUrl %>"><%=Model.SpeakerName %></a></p>
<p class="display info"><%=Model.SpeakerBio%></p>
 <p class="display"><span class="display-label">Meeting Info:</span><%=Model.MeetingInfo %></p>
  

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
