<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.TrackEditView" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<%@ Import Namespace="CodeCampServer.Core.Common" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="CodeCampServer.UI" %>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">

	<script type="text/javascript" src="/scripts/jqModal.js"></script>

	<script type="text/javascript" src="/scripts/jquery.form.js"></script>

	<link id="jqModalCss" rel="Stylesheet" type="text/css" media="all" href="/css/jqModal.css"
		runat="server" />

	</asp:Content>
<asp:Content ContentPlaceHolderID="Main" runat="server">
		<form action="<%= Url.Action<SpeakerController>(x => x.Save(null)) %>" method="post">
	<div>
		<h1>
			Edit Track</h1>
		<%=Errors.Display()%>
		<table class="dataEntry">
			<tr>
				<td class="w50p">
					<%= InputFor(x => x.Id) %>
					<%= InputFor(x => x.ConferenceId) %>
					<%= InputFor(x => x.ConferenceKey) %>
					<%=InputFor(a => a.Name)%>
				</td>
			</tr>
		</table>
		<br />
		<br />
		<div class="p10 tac">
				<%=Html.SubmitButton("save", "Save", new{@class="pr10 w100"}) %>    
				<a href="<%=Url.Action<AdminController>(x => x.Index()).ToXHTMLLink() %>"  class="pr10 mt5" rel="cancel">Cancel</a>				
	    </div>
	</div>
	</form>
</asp:Content>
