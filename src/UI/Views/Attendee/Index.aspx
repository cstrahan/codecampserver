<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="MvcContrib"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
	 <h2>Attendees
		<%if (User.Identity.IsAuthenticated){%>
			<a class="" href="<%=Url.Action<AttendeeController>(c=>c.New(null))%>" title="Add a new Attendee"><img src="/images/icons/application_add.png" /></a>
		<%}%>
	</h2>
  <%var attendees = (AttendeeForm[])ViewData.Model; %>
  <div class="dataContainerQuadWide mt10">
	 <div class="cleaner"></div>
	 
		  <% var counter = 0;
	   foreach (var attendee in attendees)
		{%>
		 <p><strong><%= attendee.FirstName%> <%= attendee.LastName%></strong>
  		 <a href="<%= attendee.Webpage%>"><%= attendee.Webpage%></a>
		</p>
		  <%
		counter++;
		 } 
		  %>
	

</asp:Content>