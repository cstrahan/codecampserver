<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<UserGroupForm>"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <form action="<%= Url.Action<UserGroupController>(x => x.Save(null)) %>" method="post"  >
        <div>
	        <h1>Edit UserGroup</h1>
            
            <%=Html.ValidationSummary()%>

	        <table class="dataEntry">
		        <tr>
			        <td class="w50p">
					    <div><%=Html.Input(a => a.Key)%></div>
    					    <div><%=Html.Input(a => a.Id)%></div>
    					    <div><%=Html.Input(a => a.Name)%></div>
    			    </td>			       
		        </tr>
	        </table>
	        <br />
	        <br />
	        <div class="p10 tac">
				<%=Html.SubmitButton("save", "Save", new{@class="pr10 w100"}) %>    
				<a href="<%=Url.Action<UserGroupController>(x => x.Index(null)).ToXHTMLLink() %>"  class="pr10 mt5" rel="cancel">Cancel</a>
			</div>
        </div>
    </form>
</asp:Content>
