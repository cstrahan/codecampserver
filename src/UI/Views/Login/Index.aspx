<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.LoginView"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">  
  <script type="text/javascript">
     Date.format = 'mm/dd/yyyy';
     $(function() {
		$('#Username').focus();
     });
     </script>  <form action="<%= Url.Action<LoginController>(x => x.Login(null)) %>" method="post">
		<div>
	    <h1>Please Log In:</h1>
      <%=Errors.Display()%>
	    <table class="dataEntry">
		    <tr>
			    <td class="w50p">
						<%=InputFor(a => a.Username)%>
						<%=InputFor(a => a.Password)%>
			    </td>
		    </tr>
		  </table>
	    <br />
	    <br />
	    <div class="p10 tac">
				<%=Html.SubmitButton("login", "Log in", new{@class="pr10 w100"}) %>    
				<a href="<%=Url.Action<HomeController>(x => x.Index()).ToXHTMLLink() %>"  class="pr10 mt5" rel="cancel">Cancel</a>				
	    </div>
</div>

</form>
</asp:Content>