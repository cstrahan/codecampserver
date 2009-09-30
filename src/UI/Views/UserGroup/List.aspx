<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="ViewPage<UserGroupInput[]>"%>
<%@ Import Namespace="CodeCampServer.UI.Helpers"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <h2>User Groups
		<%if (User.Identity.IsAuthenticated) { %>
		    <%= Html.ImageLink<UserGroupController>(c=>c.New(), "~/images/icons/application_add.png", "Add a new User Group") %>				
		<%}%>
    </h2>
		<table class="default datatable">
		    <thead>
		        <tr>
		            <th>User Group</th>
		            <th>Location</th>
		            <%if (User.Identity.IsAuthenticated){%>
		            <th>Action</th>
		            <%}%>
		        </tr>
		    </thead>		
		    <tbody>
	            <% foreach (var userGroup in Model) { %>
		        <tr>
		            <td>
		            <a href="http://<%=userGroup.DomainName+":"+ ViewContext.HttpContext.Request.Url.Port%>" title="Goto UserGroup site" title="<%= userGroup.Name%> <%= userGroup.Key %>"><%=Html.Encode( userGroup.Name)%></a>
		            </td>
		            <td><%=userGroup.City%> <%=userGroup.Region%> <%=userGroup.Country%></td>		            
            		<%if (User.Identity.IsAuthenticated){%>
		            <td>		            
		                <a href="<%=Url.Action<UserGroupController>(c=>c.Index(null),new {UserGroup = userGroup.Key})%>" title="View User Group " title="<%= userGroup.Name%> <%= userGroup.Key %>">View <%=Html.Encode( userGroup.Name)%></a>
		                <a href="http://<%=userGroup.DomainName+":"+ ViewContext.HttpContext.Request.Url.Port%>" title="Goto UserGroup site" title="<%= userGroup.Name%> <%= userGroup.Key %>">Goto the website: <%=Html.Encode( userGroup.Name)%></a>
			            <div class="fr"><%Html.RenderPartial("EditUserGroupLink",userGroup); %></div>			            
                    </td>
                    <%}%>
		        </tr>
	<% } %>
		    </tbody>
		</table>
</asp:Content>