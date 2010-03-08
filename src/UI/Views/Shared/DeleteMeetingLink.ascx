<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<IGloballyUnique>" %>
<%@ Import Namespace="CodeCampServer.Core.Bases"%>
<%@ Import Namespace="CodeCampServer.Core.Common" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated)
  {%>
  <form method="get" class="editButtonForm" action="<%=Url.Action<MeetingController>(t => t.Delete(null,null)) %>">
	<%=	Html.Hidden("meeting", Model.Id) %>
	<button id="deleteMeeting<%=Model.Id%>" class="ui-state-default ui-corner-all fg-button-icon-solo" type="submit"
		title="Delete Meeting" >
	<span class="ui-icon ui-icon-minus" />
</button>
</form>

<script type="text/javascript">
  $(function() {
    $('#deleteMeeting<%=Model.Id%>').click(function() {
      return confirm('Are you sure you want to delete?');
    });
  });
</script>

<%}%>
