<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master"
 AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<LoginForm>"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">  
  <script type="text/javascript">     
     $(function() {
		$('#Username').focus();
     });
     </script>
       <form action="<%= Url.Action<LoginController>(x => x.Login(null)) %>" method="post">
		<div>
	    <h1>Please Log In:</h1>
      <%=Errors.Display()%>
	    <table class="dataEntry">
		    <tr>
			    <td class="w50p">
						<%=Html.Input(f=>f.Username) %>
						<%=Html.Input(f=>f.Password) %>
			    </td>
		    </tr>
		  </table>
	    <br />
	    <br />
	    <div class="p10 tac">
				<%=Html.SubmitButton("login", "Log in", new{@class="pr10 w100"}) %>    
				<a href="<%=Url.Action<HomeController>(x => x.Index(null)).ToXHTMLLink() %>"  class="pr10 mt5" rel="cancel">Cancel</a>				
	    </div>
</div>

</form>
</asp:Content>