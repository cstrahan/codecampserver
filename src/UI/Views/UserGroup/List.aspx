<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<UserGroupForm[]>"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" runat="server">
<% Html.RenderPartial("HomeMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  	<script type="text/javascript" language="javascript" src="/scripts/jquery.dataTables.js"></script>
		<script type="text/javascript" charset="utf-8">
		    $(document).ready(function() {
		    $('#listTable').dataTable();
		    });
		</script>
		
		
  <div class="dataContainerQuadWide mt10">
<h2>User Groups
		<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<UserGroupController>(c=>c.New())%>" title="Add a new User Group"><img src="/images/icons/application_add.png" /></a>
		<%}%>
</h2>

	 <div class="cleaner"></div>
	 
		<table id="listTable" class="w90p" >
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
		            <td><a href="http://<%=userGroup.Key+":"+this.ViewContext.HttpContext.Request.Url.Port%>" title="View User Group " title="<%= userGroup.Name%> <%= userGroup.Key %>"> <%=Html.Encode( userGroup.Name)%></a></td>
		            <td><%=userGroup.City%> <%=userGroup.Region%> <%=userGroup.Country%></td>		            
            		<%if (User.Identity.IsAuthenticated){%>
		            <td>
			            <div class="fr"><%Html.RenderPartial("EditUserGroupLink",userGroup); %></div>
                    </td>
                    <%}%>
		        </tr>
	<% } %>
		    </tbody>
		</table>
</div>
</asp:Content>