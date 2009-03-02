<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<TimeSlotForm>"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>


<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
    <form action="<%= Url.Action<TimeSlotController>(x => x.Save(null,null,null)) %>" method="post"  >
        <div>
	        <h1>Edit Time Slot</h1>
            
            <%=Errors.Display()%>

	        <table class="dataEntry">
		        <tr>
			        <td class="w50p">
                	    <%=Html.Input(a => a.Id)%>
                	    <%=Html.Input(a => a.ConferenceId)%>          	
                	    <%=Html.Input(a => a.ConferenceKey)%> 
					    <div><%=Html.Input(a => a.StartTime)%></div><br />
					    <div><%=Html.Input(a => a.EndTime)%></div>
						<input type="hidden" name="urlreferrer" value="<%=ViewData["UrlReferrer"].ToString().ToXHTMLLink()%>" />
			        </td>
		        </tr>
	        </table>
	        <br />
	        <br />
	        <div class="p10 tac">
						<%=Html.SubmitButton("save", "Save", new{@class="pr10 w100"}) %>    
						<a href="<%=ViewData["UrlReferrer"].ToString().ToXHTMLLink() %>"  class="pr10 mt5" rel="cancel">Cancel</a>				
					</div>
        </div>
    </form>
</asp:Content>