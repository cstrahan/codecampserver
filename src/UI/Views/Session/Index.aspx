<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.SessionEditView" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>


<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
	 <div>
		    <h1><%=ViewData.Model.Title%> <%Html.RenderPartial("EditSessionLink", ViewData.Model); %></h1>
		    
		    <div>  
						<p>Track: <%=Model.Track.Name %> Level: <%= Model.Level.DisplayName %> Room: <%=ViewData.Model.RoomNumber %></p>
						<p>Time: <%=Model.TimeSlot.StartTime%> to <%=ViewData.Model.TimeSlot.EndTime%></p>
						<p>Speaker: <a href="<%=Url.Action<SpeakerController>(c=>c.Index(null),new {speaker = Model.Speaker.Id})%>"><%=Model.Speaker.FirstName %> <%=ViewData.Model.Speaker.LastName %></a></p>
						<p>Session Abstract: <%=Model.Abstract %></p>
						<p><a href="<%= Model.MaterialsUrl %>">Session Materials</a></p>
			</div>
        </div>
</asp:Content>
