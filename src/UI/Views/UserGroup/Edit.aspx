<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<UserGroupForm>"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
<script type="text/javascript" src="/scripts/tiny_mce/tiny_mce.js"></script>
<script type="text/javascript">
    tinyMCE.init({
        mode: "textareas",
        theme: "advanced",
        theme_advanced_buttons1: "justifyleft,justifycenter,justifyright,bold,italic,bullist,numlist,indent,outdent,fontsizeselect,forecolor,fontselect,code",
        theme_advanced_buttons2: "",
        plugins: "style",
        theme_advanced_buttons1_add: "styleprops"
    });
</script>
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
    					    <div><%=Html.Input(a => a.City)%></div>
    					    <div><%=Html.Input(a => a.Region)%></div>
    					    <div><%=Html.Input(a => a.Country)%></div>
    					    <div><%=Html.Input(a => a.HomepageHTML, new { rows = 20, @class = "w75p h300" }.ToDictionary())%></div>
    					    <div><%=Html.Input(a => a.GoogleAnalysticsCode)%></div>
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
