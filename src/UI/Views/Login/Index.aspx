<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master"
 AutoEventWireup="true" Inherits="ViewPage<LoginForm>"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">  

<fieldset>
  <h3>Please log in</h3>
  <%=Html.ValidationSummary() %>
  
  <form action="/Login" method="post"> 
    <label for="Username"><span class="required">*</span> Username:</label> 
    <%=Html.TextBox("Username") %><%=Html.ValidationMessage("Username", "*") %>
    <div class="cleaner"></div> 
    
    <label for="Password"><span class="required">*</span> Password:</label> 
    <%=Html.Password("Password") %><%=Html.ValidationMessage("Password", "*")%>
    <div class="cleaner"></div> 
    
    <label></label> 
    <input type="submit" Value="Submit" /> 
    <div class="cleaner"></div> 
  </form> 
  <%=Html.ActionLink<HomeController>("Cancel", x=>x.Index(null)) %>		
  		
</fieldset>	
                	
</asp:Content>