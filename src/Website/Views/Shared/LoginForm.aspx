<%@ Page Language="C#" Title="Conference Details" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<h1>test</h1>
    <div class="login-form">
		<h3>Login</h3>
		<form action='<%= Url.Action("process", "login") %>' method="post">		    
		<table>
			<tr>
				<td><label for="email">Email:</label></td>
				<td><%=Html.TextBox("email") %></td>
			</tr>
			<tr>
				<td><label for="password">Password:</label></td>
				<td><%=Html.Password("password") %></td>
			</tr>
			<%= Html.Hidden("redirectUrl", Request.Url.AbsolutePath) %>
			<tr>
			    <td></td>
			    <td><%=Html.SubmitButton("login", "Login") %></td>
			</tr>
        </table>
        
        </form>
    </div>
</asp:Content>
