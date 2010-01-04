<%@ Page Title="" Language="C#" Inherits="ViewPage<PropertyViewModel<IEnumerable<SelectListItem>>>" %>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
	<fieldset>
		<legend><%=Model.Label%></legend>
		<table>
			<%
				var items = Model.Value.ToArray();
				for(int count = 0; count < items.Length; count++)
				{
					var item = items[count];
					var isLast = count + 1 == items.Count();
					var itemId = item.Text.Replace(" ", string.Empty);
			%>
				<%	if (count % 3 == 0) { %>
					<tr>
				<% } %>
				<td>
						<div>
							<label style="float:left; text-align: right; white-space: normal; width: auto;" for="<%= itemId %>"><%= item.Text %>:</label>
							<input type="checkbox" class="<%= itemId %>" id="<%= item.Value %>" name="<%= Model.Label %>" value="<%= item.Value %>" <%= item.Selected ? "checked=\"checked\"" : string.Empty %> />
						</div>
						<br />
				</td>
				<%	if (count % 3 == 2 || isLast) { %>
					</tr>
				<% } %>
			<% } %>
		</table>
	</fieldset>
</asp:Content>
