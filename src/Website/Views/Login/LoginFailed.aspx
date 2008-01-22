<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage" Title="CodeCampServer - Login Failed" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div style="text-align:center">
		<h3 class="error">Login Failed!</h3>
		<br />
		<p>We're sorry, but we failed to find an account with this username and password.</p>
    </div>
</asp:Content>