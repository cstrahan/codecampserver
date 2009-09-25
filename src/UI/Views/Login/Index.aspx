<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master"
 AutoEventWireup="true" Inherits="ViewPage<LoginForm>"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>


<asp:Content ContentPlaceHolderID="Main" runat="server">  
       <form action="<%= Url.Action<LoginController>(x => x.Login(null)) %>" method="post">
		
	    <h2>Please Log In:</h2>
        <%=Html.ValidationSummary()%>
        
        <fieldset id="login">
            <h3>Login Details</h3>
            
            <%=Html.Input(f=>f.Username) %>
		    <%=Html.Input(f=>f.Password) %>
		    
		    <p class="buttons"><%=Html.SubmitButton("login", "Log in") %>
		        <%= Html.ActionLink<HomeController>("Cancel", x => x.Index(null)) %>			   
			</p>
        </fieldset>
                	
</form>
</asp:Content>