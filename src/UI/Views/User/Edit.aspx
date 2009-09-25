<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="ViewPage<UserForm>"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">

<fieldset>
<h3>Edit User</h3>
  <%=Html.InputForm() %>
  <%=Html.ActionLink<AdminController>("Cancel", x=>x.Index(null)) %>				
</fieldset>			

</asp:Content>