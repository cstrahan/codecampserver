<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="ViewPage<UserGroupInput>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">

	<script type="text/javascript" src="/scripts/tiny_mce/tiny_mce.js"></script>

	<script type="text/javascript">
		tinyMCE.init({
			mode: "textareas",
			theme: "advanced",
			theme_advanced_buttons1: "justifyleft,justifycenter,justifyright,bold,italic,bullist,numlist,indent,outdent,fontsizeselect,forecolor,fontselect,code",
			theme_advanced_buttons2: "",
			plugins: "style",
			theme_advanced_buttons1_add: "styleprops",
			verify_html: false
		});
	</script>

</asp:Content>
<asp:Content ContentPlaceHolderID="Main" runat="server">
	<% using (Html.BeginForm())
	{ %>
	<%= Html.ValidationSummary() %>
	<h3>
		Edit User Group</h3>
	<div class="onpage-form ui-dialog ui-widget ui-corner-all">
		<div class="ui-widget-content ui-corner-bottom">
			<div class="ui-dialog-content">
				<%=Html.Input(a => a.Key)%>
				<%=Html.Input(a => a.DomainName)%>
				<%=Html.Input(a => a.Id)%>
				<%=Html.Input(a => a.Name)%>
				<%=Html.Input(a => a.City)%>
				<%=Html.Input(a => a.Region)%>
				<%=Html.Input(a => a.Country)%>
				<%=Html.Input(a => a.Users)%>
				<%=Html.Input(a => a.HomepageHTML).Partial("MultilineText")%>
				<%=Html.Input(a => a.GoogleAnalysticsCode)%>
				<p class="buttons">
					<input type="submit" value="Save" class="accept ui-state-default ui-corner-all ui-priority-primary "  />
					<%= Html.ActionLink<UserGroupController>( "Cancel",x=>x.List()) %>
				</p>
			</div>
		</div>
	</div>
	<% } %>
</asp:Content>
