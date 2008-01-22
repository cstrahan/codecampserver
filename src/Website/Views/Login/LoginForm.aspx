<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage" Title="CodeCampServer - Login" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div style="text-align:center">
		<h3>Login</h3>
		<% 
		    FormExtensions.SimpleForm form = Html.Form("process", "login"); %>
		<table style="text-align:left">
			<tr>
				<td>Email:</td>
				<td><%=Html.TextBox("email") %></td>
			</tr>
			<tr>
				<td>Password:</td>
				<td><%=Html.Password("password") %></td>
			</tr>
			<tr>
			    <td></td>
			    <td><%=Html.SubmitButton("login","Login") %></td>
			</tr>
        </table>
    </div>
</asp:Content>
