<%@ Page Title="" Language="C#" 
Inherits="ViewPage<ModelProperty<IEnumerable<SelectListItem>>>" %>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>
<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
<%
    ViewData.Add(Model.Name, Model.Value.Where(item => item.Selected).Select(item => item.Value).ToList());
%>
<%=Html.ListBox(Model.Name,Model.Value)%>
</asp:Content>
