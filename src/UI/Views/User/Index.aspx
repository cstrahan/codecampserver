<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<UserForm[]>"%>
<%@ Import Namespace="CodeCampServer.UI.Helpers"%>
<%@ Import Namespace="MvcContrib.UI.Grid"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
	 <h2>Users
		<%if (User.Identity.IsAuthenticated){%>
		    <%= Html.ImageLink<UserController>(c=>c.New(), "~/images/icons/application_add.png", "Add a new user") %>			
		<%}%>
	</h2>
    
    <table class="default datatable">
        <tr>
            <th>Username</th>
            <th>Name</th>
        </tr>
        <% foreach(var user in Model) { %>
        <tr>
            <td><%= Html.ActionLink<UserController>(c=>c.Edit(null), user.Username, new{user=user.Id}) %></td>
            <td><%= user.Name %></td>
        </tr>
        <% } %>
    </table>
        
</asp:Content>