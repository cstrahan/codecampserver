<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<ConferenceForm>" %>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>

		<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
				<a id="deleteConference<%=Model.Key%>" 
				    href="<%= Url.Action<ConferenceController>(t => t.Delete(null), new{conference = Model.Id}).ToXHTMLLink() %>">
				    <img src="/images/icons/application_delete.png" title="Delete the conference" /></a>
                <script  type="text/javascript">
                    $(function() {
                    $('#deleteConference<%=Model.Key%>').click(function() {
                            return confirm('Are you sure you want to delete?');
                        });
                    });
                </script>
				
		<%}%>		
