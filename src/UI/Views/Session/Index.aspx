<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.SessionEditView" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<%@ Import Namespace="CodeCampServer.UI"%>

<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
	 <div>
		    <h1><%=ViewData.Model.Title%></h1>  
			<table class="dataEntry">
				<tr>
					<td class="w50p">
						Track: <%=ViewData.Model.Track.Name %> Level: <%=ViewData.Model.Level.DisplayName %> Room: <%=ViewData.Model.RoomNumber %><br />
						Time: <%=ViewData.Model.TimeSlot.StartTime%> to <%=ViewData.Model.TimeSlot.EndTime%> <br />
						Speaker: <%=ViewData.Model.Speaker.FirstName %> <%=ViewData.Model.Speaker.LastName %><br />
						Session Abstract: <%=ViewData.Model.Abstract %><br />
						Session Materials: <%=ViewData.Model.MaterialsUrl %><br />
					</td>
				</tr>
			</table>
        </div>
</asp:Content>
