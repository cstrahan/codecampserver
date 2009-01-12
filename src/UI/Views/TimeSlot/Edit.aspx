<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<%@ Import Namespace="CodeCampServer.UI"%>

<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
  <% var form = (TimeSlotForm)  ViewData.Model; %>

    <form action="<%= Url.Action<TimeSlotController>(x => x.Save(null,null)) %>" method="post"  >
        <div>
	        <h1>Edit Time Slot</h1>
            
            <%=Errors.Display()%>

	        <table class="dataEntry">
		        <tr>
			        <td class="w50p">
                	    <%=InputFor<TimeSlotForm>(a => a.Id)%>
                	    <%=InputFor<TimeSlotForm>(a => a.ConferenceId)%>          	
                	    <%=InputFor<TimeSlotForm>(a => a.ConferenceKey)%> 
					    <div><%=InputFor<TimeSlotForm>(a => a.StartTime)%></div><br />
					    <div><%=InputFor<TimeSlotForm>(a => a.EndTime)%></div>					    
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