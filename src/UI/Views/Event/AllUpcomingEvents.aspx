<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="ViewPage<EventList[]>" %>
<div  class="post"><h1>Upcoming Events</h1></div>
<% foreach (EventList eventKey in Model){%>
    <div class="post"> 
			<h2><a href="http://<%=eventKey.UserGroupDomainName%>"><%=eventKey.Title %></a></h2>
			<%=eventKey.UserGroupName %><br />
			<%=eventKey.Date %>
		</div> 
		<hr /> 
<%}%>
