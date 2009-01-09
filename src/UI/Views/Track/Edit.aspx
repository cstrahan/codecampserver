<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.TrackEdit"%>
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

    <form action="<%= Url.Action<TrackController>(x => x.Save(null)) %>" method="post"  >
      
    <input type="hidden" value="<%= ViewData.Model.Id %>" name="Id" />
    <input type="hidden" value="<%= ViewData.Model.ConferenceId %>" name="ConferenceId" />
    <input type="hidden" value="<%= ViewData.Model.ConferenceKey %>" name="ConferenceKey" />
    
	 <div><%=InputFor(a => a.Name)%></div>
	
	<a href="<%=Url.Action<TrackController>(x => x.Index(null)).ToXHTMLLink() %>"  class="fr pr10 mt5" rel="cancel">Cancel</a>
	<input type="image" alt="Save" class="fr pr10" src="/images/Buttons/save.gif" id="save" />

</form>
</asp:Content>