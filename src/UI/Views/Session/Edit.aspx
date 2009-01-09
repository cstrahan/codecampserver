<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.SessionEditView"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
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

    <form action="<%= Url.Action<SessionController>(x => x.Save(null)) %>" method="post"  >
      
      <%=InputFor(f=>f.Id) %>
      <%=InputFor(f=>f.Conference) %>
      <%=InputFor(f=>f.Title) %>
      <%=InputFor(f=>f.Key) %>
      <%=InputFor(f=>f.Speaker) %>
      <%=InputFor(f=>f.Abstract) %>
      <%=InputFor(f=>f.Level) %>
      <%=InputFor(f=>f.Track) %>
      <%=InputFor(f=>f.TimeSlot) %>
      <%=InputFor(f=>f.RoomNumber) %>
      <%=InputFor(f=>f.MaterialsUrl) %>
	
	<a href="<%=Url.Action<SessionController>(x => x.Index(null)).ToXHTMLLink() %>"  class="fr pr10 mt5" rel="cancel">Cancel</a>
	<%=Html.SubmitImage("save", "/images/Buttons/save.gif", new { @class = "fr pr10" })%>

</form>
</asp:Content>