<%@ Page Title="" Language="C#" 
Inherits="ViewPage<PropertyViewModel<IEnumerable<SelectListItem>>>" %>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>
<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
	<%=Html.DropDownList(Model.Name, Model.Value)%>
</asp:Content>
