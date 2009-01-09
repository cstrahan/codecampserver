<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.AdminEditView"%>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<%@ Import Namespace="CodeCampServer.UI"%>

<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
	<script type="text/javascript" src="/scripts/jqModal.js"></script>
	<script type="text/javascript" src="/scripts/jquery.form.js"></script>
	<link id="jqModalCss" rel="Stylesheet" type="text/css" media="all" href="/css/jqModal.css" runat="server" />
	<script type="text/javascript">
		$().ready(function() {
		$('#submitButton').click(function() { $('form').submit() });
		$('.modal').jqm({ modal: true });
		$('.modal').jqmShow();
	});
	
	</script>
</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
  <p><a href="<%= Url.Action("index", "search") %>" class="searchWithReflectionButton">Search</a></p>

<form action="<%= Url.Action<AdminController>(x => x.Save(null)) %>" method="post"  >

<div class="modal jqmWindow">

	    <h1 class="heading">Edit Admin Record</h1>
        
        <%=Errors.Display()%>

	    <table class="dataEntry">
		    <tr>
			    <th class="w50p">
            <%=InputFor(a => a.Id)%>            	
						<%=InputFor(a => a.Username)%>
						<%=InputFor(a => a.Name)%>
						<%=InputFor(a => a.EmailAddress)%>
						<%=InputFor(a => a.Password)%>
						<%=InputFor(a => a.ConfirmPassword)%>
			    </th>
			    <th class="w50p">
				    <div></div>
			    </th>
		    </tr>
	    </table>
	    <br />
	    <br />
	    <div class="fr p10">
	        <a href="<%=Url.Action<AdminController>(x => x.Index()).ToXHTMLLink() %>"  class="fr pr10 mt5" rel="cancel">Cancel</a>
	        <input type="image" alt="Save" class="fr pr10" src="/images/Buttons/save.gif" id="save" />
	    </div>
</div>

</form>

</asp:Content>