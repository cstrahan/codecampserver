<%@ Page Language="C#" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.BaseViewPage<string[]>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>
    <div class="addMeeting"><%Html.RenderPartial("AddMeetingLink"); %></div>
    <style type="text/css">
h1.title
{
font-size:1.8em;
}
.post
{
padding: 0px 20px;
margin-bottom: 20px;
}

.post .title
{
margin-bottom: 5px;
padding-bottom: 5px;
}

.post h1
{
width: 520px;
padding: 0px 0 0 0px;
font-size: 24px;
color: #28313A;
}

.post h2
{
width: 520px;
padding: 0px 0 0 0px;
font-size: 22px;
color: #28313A;
}

.post .entry p b
{
font-size:15px;
}

.addMeeting
{
float: right;
}
</style>
    
<% foreach (string eventKey in Model){%>
    <div class="post"> 
			<%Html.RenderAction("announcement", "event", new { @event = eventKey }); %>
		</div> 
		<hr style="clear: both" /> 
<%}%>
