<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<UserForm[]>"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
	 <h2>Users
		<%if (User.Identity.IsAuthenticated){%>
			<a class="" href="<%=Url.Action<UserController>(c=>c.New())%>" title="Add a new User"><img src="/images/icons/application_add.png" /></a>
		<%}%>
	</h2>
  <%var users = ViewData.Model;%>
  <div class="dataContainerQuadWide mt10">
	 <div class="cleaner"></div>
		<%foreach (var user in users){%>
		 <p><strong><%= user.Name%></strong>
  		 <a href="<%= Url.Action<UserController>(c=>c.Edit(null),new{user=user.Id})%>"><%= user.Username%></a>
		</p>
	  <%}%>
</asp:Content>