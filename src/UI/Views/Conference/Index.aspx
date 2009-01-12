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
	<script type="text/javascript" src="/scripts/jqModal.js"></script>
	<script type="text/javascript" src="/scripts/jquery.form.js"></script>
	<link id="jqModalCss" rel="Stylesheet" type="text/css" media="all" href="/css/jqModal.css" runat="server" />
</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
        <div>
	        <h1>Conference Details</h1>
            
            <%=Errors.Display()%>

	        <table>
		        <tr>
			        <td class="w50p">
								Description:  <%=ViewData.Model.Description %>
								Start Date:  <%=ViewData.Model.StartDate %>
								End Date:  <%=ViewData.Model.EndDate %>
								Location:  <%=ViewData.Model.LocationName %>
			        </td>
		        </tr>
	        </table>
        </div>
    </form>
</asp:Content>