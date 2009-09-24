<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="menu"> 
    <ul> 
        <asp:LoginView ID="LoginView1" runat="server">
	        <LoggedInTemplate>
		        <li><a href="<%=Url.Action<AdminController>(c=>c.Index(null)) %>">admin</a></li>
	        </LoggedInTemplate>
        </asp:LoginView>
        <%if (!ViewData.Get<UserGroup>().IsDefault())
          { %>
        <li><a href="<%=Url.Action<HomeController>(c=>c.Index(null)) %>">upcoming events</a></li>
        <li><a href="<%=Url.Action<HomeController>(c=>c.Events(null)) %>">all events</a></li>        
        <li><a href="<%=Url.Action<HomeController>(c=>c.About(null)) %>">about us</a></li>
        <%} %>
    </ul> 
</div> 


