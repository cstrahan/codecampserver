<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.AttendeeEditView"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>


<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
	<script type="text/javascript" src="/scripts/jqModal.js"></script>
	<script type="text/javascript" src="/scripts/jquery.form.js"></script>
	<link id="jqModalCss" rel="Stylesheet" type="text/css" media="all" href="/css/jqModal.css" runat="server" />
</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
  <script type="text/javascript">     
     $(function() {
		$('#FirstName').focus();
     });
     </script>
    <form action="<%= Url.Action<AttendeeController>(x => x.Save(null)) %>" method="post"  >
        <div>
	        <h1>Register</h1>
            
            <%=Errors.Display()%>
	        <table class="dataEntry">
	        <tr><th></th></tr>
		        <tr>
			        <td class="w50p">       	
			        	<%=Html.Input(a => a.AttendeeID)%>
						<%=Html.Input(a => a.ConferenceID)%>
					    <%=Html.Input(a => a.FirstName)%>
					    <%=Html.Input(a => a.LastName)%>
					    <%=Html.Input(a => a.EmailAddress)%>
					    <%=Html.Input(a => a.Webpage)%>	
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