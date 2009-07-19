<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master"
 AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<LoginForm>"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" runat="server">
<% Html.RenderPartial("HomeMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">  
       <form action="<%= Url.Action<LoginController>(x => x.Login(null)) %>" method="post">
		
	    <h2>Please Log In:</h2>
        <%=Errors.Display()%>
        
        <fieldset id="login">
            <legend>Login Details</legend>
            
            <%=Html.Input(f=>f.Username) %>
		    <%=Html.Input(f=>f.Password) %>
		    
		    <p class="buttons"><%=Html.SubmitButton("login", "Log in") %>
		        <%= Html.ActionLink<HomeController>("Cancel", x => x.Index(null, null)) %>			   
			</p>
        </fieldset>
                	
</form>
</asp:Content>