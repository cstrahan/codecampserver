<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.ConferenceEditView"%>
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
    <form action="<%= Url.Action<ConferenceController>(x => x.Save(null)) %>" method="post"  >
        <div>
	        <h1>Edit Conference</h1>
            
            <%=Errors.Display()%>

	        <table class="dataEntry">
		        <tr>
			        <td class="w50p">
									<%=InputFor(a => a.Id)%>            	
					    <%=InputFor(a => a.Name)%>
					    <%=InputFor(a => a.Key)%>
					    <%=InputFor(a => a.Description)%>
					    <%=InputFor(a => a.StartDate)%><br />
					    <%=InputFor(a => a.EndDate)%><br />
					    <%=InputFor(a => a.LocationName)%>
					    <%=InputFor(a => a.Address)%>
					    <%=InputFor(a => a.City)%>
					    <%=InputFor(a => a.Region)%>
					    <%=InputFor(a => a.PostalCode)%>
					    <%=InputFor(a => a.PhoneNumber)%>
			        </td>
		        </tr>
	        </table>
	        <br />
	        <br />
	        <div class="p10 tac">
						<%=Html.SubmitButton("save", "Save", new{@class="pr10 w100"}) %>    
						<a href="<%=Url.Action<HomeController>(x => x.Index()).ToXHTMLLink() %>"  class="pr10 mt5" rel="cancel">Cancel</a>				
					</div>
        </div>
    </form>
</asp:Content>