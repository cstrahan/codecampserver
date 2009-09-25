<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="ViewPage<UserForm>"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">

<% using(Html.BeginForm("Save")) { %>
        <%=Html.ValidationSummary()%>
        
<fieldset>
<h3>Edit User</h3>

  <%=Html.Input(a => a.Id)%>            	
  <%=Html.Input(a => a.Username)%>
  <%=Html.Input(a => a.Name)%>
  <%=Html.Input(a => a.EmailAddress)%>
  <%=Html.Input(a => a.Password)%>
  <%=Html.Input(a => a.ConfirmPassword)%>

  <p class="buttons">
    <input type="submit" value="Save" />
    <%= Html.ActionLink<AdminController>("Cancel", x=>x.Index(null)) %>				
  </p>
</fieldset>			

<% } %>

</asp:Content>