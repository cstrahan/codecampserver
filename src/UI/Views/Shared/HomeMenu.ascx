<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
    <ul class="menu"> 
        <asp:LoginView ID="LoginView1" runat="server">
	        <LoggedInTemplate>
		        <li><a href="<%=CodeCampServer.UI.Helpers.Extensions.UrlHelperExtensions.Action<AdminController>(Url, c=>c.Index(null)) %>" rel="<%=CodeCampSite.Navigation.Admin %>"><b>admin</b></a></li>
	        </LoggedInTemplate>
        </asp:LoginView>
        <%if (ViewData.Contains<UserGroup>())
          { %>
        <li><a href="<%=CodeCampServer.UI.Helpers.Extensions.UrlHelperExtensions.Action<HomeController>(Url, c=>c.Index(null)) %>"><b>upcoming events</b></a></li>
        <li><a href="<%=CodeCampServer.UI.Helpers.Extensions.UrlHelperExtensions.Action<HomeController>(Url, c=>c.Events(null)) %>"><b>all events</b></a></li>        
        <li><a href="<%=CodeCampServer.UI.Helpers.Extensions.UrlHelperExtensions.Action<HomeController>(Url, c=>c.About(null)) %>"><b>about us</b></a></li>
        <%} %>
    </ul> 


