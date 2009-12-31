<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<IGloballyUnique>" %>
<%@ Import Namespace="CodeCampServer.Core.Common" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated)
  {%>
<a id="deleteMeeting<%=Model.Id%>" href="<%= Url.Action<MeetingController>(t => t.Delete(null,null), new{meeting = Model.Id}).ToXHTMLLink() %>">
  <img src="/images/icons/application_delete.png" title="Delete the meeting" alt="Delete the Meeting" /></a>

<script type="text/javascript">
  $(function() {
    $('#deleteMeeting<%=Model.Id%>').click(function() {
      return confirm('Are you sure you want to delete?');
    });
  });
</script>

<%}%>
