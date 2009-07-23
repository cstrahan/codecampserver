<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<SessionForm[]>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">

<h2><%=ViewData.Get<PageInfo>().SubTitle%> / Sessions
		<%if (User.Identity.IsAuthenticated){%>
		    <a href='<%= Url.Action<SessionController>(x=>x.New(null)) %>'>
		        <%= Html.Image(Url.Content("~/images/icons/application_add.png"), "Add New Session") %>
		    </a>		    		            
		<%}%>
</h2>

		<table class="default datatable">
		    <thead>
		        <tr>
		            <th>Track</th>
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
		            
		            <td><%=Html.Encode( session.TimeSlot.StartTime.ToString())%></td>
		            <td><%=Html.Encode( session.TimeSlot.EndTime.ToString())%></td>
		            
		            <td>
		                <%= Html.RouteLink(session.Title, "session", new { sessionKey=session.Key }, new { title= session.TimeSlot.StartTime + " " + session.Key}) %><br />
		                (Level <%= session.Level %>) - <%= session.Speaker.FirstName %> <%= session.Speaker.LastName %>
		            </td>
		            		    
            		<%if (User.Identity.IsAuthenticated){%>
		            <td valign="middle">
		                <div class="fr pl10"><%Html.RenderPartial(PartialViews.Shared.DeleteSessionLink,session); %></div>
			            <div class="fr"><%Html.RenderPartial(PartialViews.Shared.EditSessionLink,session); %></div>
                    </td>
                    <%}%>
		        </tr>
	<% } %>
		    </tbody>
		</table></asp:Content>
