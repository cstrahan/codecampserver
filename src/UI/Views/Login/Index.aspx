<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master"
 AutoEventWireup="true" Inherits="ViewPage<LoginForm>"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">  

<fieldset>
  <h3>Please log in</h3>
  <%=Html.InputForm() %>
  <%=Html.ActionLink<HomeController>("Cancel", x=>x.Index(null)) %>				
</fieldset>	
                	
</asp:Content>