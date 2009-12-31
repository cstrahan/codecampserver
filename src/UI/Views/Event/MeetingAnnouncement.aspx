<%@ Page Language="C#" AutoEventWireup="true" Inherits="ViewPage<MeetingAnnouncementDisplay>" %>

<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>
<h1 class="title">
	<%= Model.Heading %>
</h1>
<%Html.RenderPartial("EditMeetingLink", Model); %>
<%Html.RenderPartial("DeleteMeetingLink", Model); %>
<div class="entry">
	<p class="display">
		<span class="display-label">When:</span>
		<%=Model.When %></p>
	<p class="display info">
		(<a href="<%=Model.LocationUrl %>"><%=Model.LocationName %></a>)</p>
	<p class="display">
		<span class="display-label">Topic:</span>
		<%=Model.Topic %></p>
	<div class="display info">
		<%=Model.Summary%></div>
	<p class="display">
		<span class="display-label">Speaker:</span> <a href="<%=Model.SpeakerUrl %>">
			<%=Model.SpeakerName %></a></p>
	<div class="display info">
		<%=Model.SpeakerBio%></div>
	<div class="display">
		<span class="display-label">Meeting Info:</span><%=Model.MeetingInfo %></div>
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
