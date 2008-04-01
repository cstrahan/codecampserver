<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" 
AutoEventWireup="true"  Inherits="System.Web.Mvc.ViewPage" 
Title="CodeCampServer - Login" %>
<%@ Import namespace="CodeCampServer.Website"%>
<%@ Import namespace="CodeCampServer.Website.Views"%>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>
<%@ Import namespace="System.Web.Mvc"%>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <% if(TempData.ContainsKey(TempDataKeys.Error)) { %>
        <span class="error"><%=TempData[TempDataKeys.Error]%></span>
    <% } %>

    <% if(ViewData.GetOrDefault("ShowFirstTimeRegisterLink", false)) { %>
    
        <div class="notice">
            <h2>Create Your Administrator Account</h2>
            <p>There are no registered users in the system.  Create your administrator account in the form below.  Be sure to remember your password,
            once there is a registered administrator, you will need the password to manage the site.</p>
        </div>                
            
        <p></p>
        <% using(Html.Form("login", "CreateAdminAccount", FormMethod.Post)) { %>
        <fieldset>
            <legend>Administrator Account</legend>
            <label for="firstName">First Name</label>
            <input type="text" name="firstName" maxlength="100" />
            
            <label for="lastName">Last Name</label>
            <input type="text" name="lastName" maxlength="100" />
            
            <label for="email">Email Address</label>
            <input type="text" name="email" maxlength="200" />
            
            <label for="password">Password</label>
            <input type="password" name="password" maxlength="100" />
            
            <label for="passwordConfirm">Confirm Password</label>
            <input type="password" name="passwordConfirm" maxlength="100" />
                            
            <div>
            <%= Html.SubmitButton("submit", "Create Account") %>
            </div>                                
        </fieldset>
        <% } %>
        <br style="clear:left"/>            
        
    
    <% } else { %>

    <div style="text-align:center">
		<h3>Login</h3>
		<% using (Html.Form("login", "process")) { %>
		<table style="text-align:left">
			<tr>
				<td>Email:</td>
				<td><%=Html.TextBox("email", 50) %></td>
			</tr>
			<tr>
				<td>Password:</td>
				<td><%=Html.Password("password", 50)%></td>
			</tr>
			<tr>
			    <td></td>
			    <td><%=Html.SubmitButton("Login") %></td>
			</tr>
        </table>
        <% } %>
    </div>
    
    <% } %>
</asp:Content>
