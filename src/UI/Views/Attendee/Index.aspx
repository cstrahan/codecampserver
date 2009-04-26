<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
	 <%var attendees = (AttendeeForm[])ViewData.Model; %>
	 <h2>Attendees 
		<%if (User.Identity.IsAuthenticated){%>
			<a class="" href="<%=Url.Action<AttendeeController>(c=>c.New(null))%>" title="Add a new Attendee"><img src="/images/icons/application_add.png" /></a>
		<%}%>
	</h2><em><%= attendees.Length %> Attending</em>
  
  <div class="dataContainerQuadWide mt10">
	 <div class="cleaner"></div>
	    <dl id="attendee-list">
		  <% var counter = 0;
	   foreach (var attendee in attendees)
		{%>
		 
		    <dt><%= attendee.FirstName%> <%= attendee.LastName%></dt>
		        
		    <dd><a class="attending-url" href="<%= attendee.Webpage%>"><%= attendee.Webpage%></a></dd>
  		    
  		    
  		    
		
		  <%
		counter++;
		 } 
		  %>
	    </dl>
    </div>
</asp:Content>