<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="ViewPage<IEnumerable>"%>
<%@ Import Namespace="CodeCampServer.UI.Helpers.Extensions"%>
<%@ Import Namespace="MvcContrib.UI.Grid"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
	<h2><%=Html.GetLabelForModel() %> </h2>
	<div class="button">
	<%Html.BeginForm("Edit",ViewContext.RouteData.Values["controller"].ToString(),FormMethod.Get ); %>
	<%=Html.ButtonAddIcon()%>
	<%Html.EndForm(); %>
	</div>
	<%= Html.SearchCountMessage(Model) %>
  <table class="fullWidth">
    <thead>
			<tr>
			<%foreach (var info in Html.VisibleProperties(Model)) {%>
                <th><%=info.GetLabel()%></th>
            <% } %>
      </tr>
		</thead>
    <tbody>
			<% 	var modelindex = 0; 
			    foreach (var model in Model)
        { %>
      <tr>
      <% var index = 0;
				foreach (var info in model.VisibleProperties()) {
				    ViewData[index.ToString()] = model;
					if (index == 0) { %>
				<td><a href="<%=Url.Action("display",new {entity= model.GetType().GetProperty("Id").GetValue(model,null)})%>"><%=info.GetValue(model, null)%></a></td>
					<% }
					else {%>
                <td><%=Html.Display(index+"."+info.Name) %></td>	
          <% }
					index++;
				} %>
        <td><%Html.BeginForm("Edit",ViewContext.RouteData.Values["controller"].ToString(),FormMethod.Get ); %>
           		 <%= Html.Hidden("entity", model.GetType().GetProperty("Id").GetValue(model, null))%>
            	 <%=Html.ButtonEditIcon()%>
	             <%Html.EndForm(); %></td>
      </tr>	
			<%
            modelindex++;
                } %>
		</tbody>
    <tfoot></tfoot>
	</table>
</asp:Content>