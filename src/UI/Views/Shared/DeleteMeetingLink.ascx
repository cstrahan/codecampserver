<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<MeetingForm>" %>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>

<%@ Import Namespace="CodeCampServer.UI.Controllers"%>

		<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
				<a id="deleteMeeting<%=Model.Key%>" 
				    href="<%= Url.Action<MeetingController>(t => t.Delete(null), new{meeting = Model.Id}).ToXHTMLLink() %>">
				<img src="/images/icons/application_delete.png" title="Delete the meeting" /></a>
                <script  type="text/javascript">
                    $(function() {
                    $('#deleteMeeting<%=Model.Key%>').click(function() {
                            return confirm('Are you sure you want to delete?');
                        });
                    });
                </script>
				
		<%}%>		
