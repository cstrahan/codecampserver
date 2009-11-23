<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="ViewPage<UserInput[]>"%>
<%@ Import Namespace="CodeCampServer.UI.Helpers"%>
<%@ Import Namespace="MvcContrib.UI.Grid"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
	 <h2>Users
		<%if (User.Identity.IsAuthenticated){%>
			<a href="<%=Url.Action<UserController>(t=>t.Edit((User)null)) %>"><img src="/images/icons/application_add.png" /></a>
		<%}%>
	</h2>
    <%=Html.Grid(Model.Select(
    		input =>new { input.Id,input.Username,input.Name}))
    			.AutoColumns().WithClass("datatable")
			.Columns(
	         builder =>{
				 
	                       builder.For("Action").
                           PartialCell("EditUserLink").
                           Visible(User.Identity.IsAuthenticated);
                    })%>
</asp:Content>