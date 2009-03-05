<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"  validateRequest="false"
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ConferenceForm>"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>


<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
<script type="text/javascript" src="/scripts/tiny_mce/tiny_mce.js"></script>
<script type="text/javascript">
    tinyMCE.init({
        mode: "textareas",
        theme: "advanced",
        theme_advanced_buttons1: "justifyleft,justifycenter,justifyright,bold,italic,bullist,numlist,indent,outdent,fontsizeselect,forecolor,fontselect",
        theme_advanced_buttons2: "",        
        plugins : "style",
	    theme_advanced_buttons1_add : "styleprops"
    });
</script>

</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
    <form action="<%= Url.Action<ConferenceController>(x => x.Save(null)) %>" method="post"  >
        <div>
	        <h1>Edit Conference</h1>
            
            <%=Errors.Display()%>

	        <table class="dataEntry">
		        <tr>
			        <td class="w50p">
									<%=Html.Input(a => a.Id)%>            	
					    <%=Html.Input(a => a.Name)%>
					    <%=Html.Input(a => a.Key)%>
					    <%=Html.Input(a => a.Description)%>
					    <%=Html.Input(a => a.StartDate)%><br />
					    <%=Html.Input(a => a.EndDate)%><br />
					    <%=Html.Input(a => a.LocationName)%>
					    <%=Html.Input(a => a.Address)%>
					    <%=Html.Input(a => a.City)%>
					    <%=Html.Input(a => a.Region)%>
					    <%=Html.Input(a => a.PostalCode)%>
					    <%=Html.Input(a => a.PhoneNumber)%>
					    <%=Html.Input(a => a.HtmlContent).Attributes(new {rows=10,@class="w75p h300"}) %>
					    
			        </td>
		        </tr>
	        </table>
	        <br />
	        <br />
	        <div class="p10 tac">
						<%=Html.SubmitButton("save", "Save", new{@class="pr10 w100"}) %>    
						<a href="<%=Url.Action<HomeController>(x => x.Index()).ToXHTMLLink() %>"  class="pr10 mt5" rel="cancel">Cancel</a>				
					</div>
        </div>
    </form>
</asp:Content>