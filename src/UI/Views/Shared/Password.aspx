<%@ Page Title="" Language="C#" 
Inherits="ViewPage<PropertyViewModel<object>>" %>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
<%=Html.Password(Model.Name,Model.Value)%>
</asp:Content>
