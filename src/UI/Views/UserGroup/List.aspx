<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="ViewPage<UserGroupInput[]>"%>
<%@ Import Namespace="CodeCampServer.UI.Helpers.Extensions"%>
<%@ Import Namespace="CodeCampServer.UI.Helpers"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <h2>User Groups    </h2>
		<%if (User.Identity.IsAuthenticated) { %>
		    <%= Html.AddImageButton(Url.Action("Edit", new { }), CodeCampSite.Admin.CreateUserGroup)%>
		<%}%>

		<table class="fullWidth" id="UserGroupInput">
		    <thead>
		        <tr>
		            <th>User Group</th>
		            <th>Location</th>
		            <%if (User.Identity.IsAuthenticated){%>
		            <th>Action</th>
		            <%}%>
		        </tr>
		    </thead>		
		    <tbody id="UserGroupInputBody">
	            <% foreach (var userGroup in Model) { %>
		        <tr>
		            <td>
		            <a href="http://<%=userGroup.DomainName+":"+ ViewContext.HttpContext.Request.Url.Port%>" title="Go To <%= userGroup.Name%> <%= userGroup.Key %>"><%=Html.Encode( userGroup.Name)%></a>
		            </td>
		            <td><%=userGroup.City%> <%=userGroup.Region%> <%=userGroup.Country%></td>		            
            		<%if (User.Identity.IsAuthenticated){%>
		            <td>		            
		                <a href="<%=Url.Action<UserGroupController>(c=>c.Index(null),new {UserGroup = userGroup.Key})%>"  title="View <%= userGroup.Name%> <%= userGroup.Key %>" rel="<%= CodeCampSite.Admin.ViewUserGroup %>">View <%=Html.Encode( userGroup.Name)%></a>
		                <a href="http://<%=userGroup.DomainName+":"+ ViewContext.HttpContext.Request.Url.Port%>"  title="Go To <%= userGroup.Name%> <%= userGroup.Key %>">Goto the website: <%=Html.Encode( userGroup.Name)%></a>
			            			            
                    </td>
                    <td>
						<div class="fr"><%Html.RenderPartial("EditUserGroupLink",userGroup); %></div>
                    </td>
                    <%}%>
		        </tr>
	<% } %>
		    </tbody>
		</table>
</asp:Content>