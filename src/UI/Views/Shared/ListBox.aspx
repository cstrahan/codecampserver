<%@ Page Title="" Language="C#" 
Inherits="ViewPage<PropertyViewModel<IEnumerable<SelectListItem>>>" %>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>
<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
<%
    ViewData.Add(Model.Name, Model.Value.Where(item => item.Selected).Select(item => item.Value).ToList());
%>
<%=Html.ListBox(Model.Name, Model.Value, new { style = string.Format("Height:{0}px;", Math.Min(Model.Value.Count()*15, 100)) })%>
</asp:Content>
