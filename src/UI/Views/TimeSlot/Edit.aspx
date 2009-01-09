<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="CodeCampServer.UI.ViewPage.BaseViewPage"%>
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

  <% var form = (TimeSlotForm)  ViewData.Model; %>

    <form action="<%= Url.Action<TimeSlotController>(x => x.Save(null,null)) %>" method="post"  >
        <div class="modal jqmWindow">
	        <h1 class="heading">Edit Time Slot</h1>
            
            <%=Errors.Display()%>

	        <table class="dataEntry">
		        <tr>
			        <th class="w50p">
                	    <%=InputFor<TimeSlotForm>(a => a.Id)%>
                	    <%=InputFor<TimeSlotForm>(a => a.ConferenceId)%>          	
                	    <%=InputFor<TimeSlotForm>(a => a.ConferenceKey)%> 
					    <div><%=InputFor<TimeSlotForm>(a => a.StartTime)%></div>
					    <div><%=InputFor<TimeSlotForm>(a => a.EndTime)%></div>					    
			        </th>
			        <th class="w50p">
				        <div></div>
			        </th>
		        </tr>
	        </table>
	        <br />
	        <br />
	        <div class="fr p10">
	            <a href="<%=Url.Action<TimeSlotController>(x => x.Index(null),new {conference=form.ConferenceKey}).ToXHTMLLink() %>"  class="fr pr10 mt5" rel="cancel">Cancel</a>
	            <input type="image" alt="Save" class="fr pr10" src="/images/Buttons/save.gif" id="save" />
	        </div>
        </div>
    </form>
</asp:Content>