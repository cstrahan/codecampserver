<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<SessionForm[]>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
		<script type="text/javascript" language="javascript" src="/scripts/jquery.dataTables.js"></script>
		<script type="text/javascript" charset="utf-8">
		    $(document).ready(function() {
		    $('#sessionTable').dataTable();
		    });
		</script>

<h2>Sessions
		<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<SessionController>(c=>c.New())%>" title="Add a new Session"><img src="/images/icons/application_add.png" /></a>
		<%}%>
</h2>

		<table id="sessionTable" class="w90p" >
		    <thead>
		        <tr>
		            <th>Track</th>
		            <th class="w100">Level</th>
		            <th>Speaker</th>
		            <th>Start Time</th>
		            <th>End Time</th>
		            <th>Title</th>
		            <%if (User.Identity.IsAuthenticated){%>
		            <th>Action</th>
		            <%}%>
		        </tr>
		    </thead>		
		    <tbody>
	            <% foreach (var session in Model) { %>
		        <tr>
		            <td><%=session.Track.Name%></td>
		            <td><%=session.Level.DisplayName%></td>
		            <td><%=session.Speaker.FirstName%> <%=session.Speaker.LastName%></td>
		            <td><%=Html.Encode( session.TimeSlot.StartTime.ToString())%></td>
		            <td><%=Html.Encode( session.TimeSlot.EndTime.ToString())%></td>
		            <td><a href="<%=Url.RouteUrl("session",new{sessionKey=session.Key}).ToXHTMLLink() %>" title="<%= session.TimeSlot.StartTime %> <%= session.Key %>"> <%=Html.Encode( session.Title)%></a></td>
		            
            		<%if (User.Identity.IsAuthenticated){%>
		            <td>			<div class="fr pl10"><%Html.RenderPartial(PartialViews.Shared.DeleteSessionLink,session); %></div>
			            <div class="fr"><%Html.RenderPartial(PartialViews.Shared.EditSessionLink,session); %></div>
                    </td>
                    <%}%>
		        </tr>
	<% } %>
		    </tbody>
		</table></asp:Content>
