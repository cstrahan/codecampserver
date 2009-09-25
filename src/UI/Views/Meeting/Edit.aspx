<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"  validateRequest="false"
Inherits="ViewPage<MeetingForm>"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">

    <% using(Html.BeginForm<MeetingController>(x=>x.Save(null))) { %>
        <fieldset id="meeting">
            <h3>Edit Meeting</h3>    
            
            <%=Html.ValidationSummary()%>

			<%=Html.Input(a => a.Id)%>            	
		    <%=Html.Input(a => a.UserGroupId)%>
		    <%=Html.Input(a => a.Name)%>
		    <%=Html.Input(a => a.Topic)%>
		    <%=Html.Input(a => a.Summary).Partial("MultilineText")%>
		    <%=Html.Input(a => a.Key)%>
		    <%=Html.Input(a => a.StartDate).Partial("DatePicker")%>
		    <%=Html.Input(a => a.EndDate).Partial("DatePicker")%>
		    <%=Html.Input(a => a.TimeZone)%>
		    <%=Html.Input(a => a.Description)%>
		    <%=Html.Input(a => a.LocationName)%>
		    <%=Html.Input(a => a.LocationUrl)%>
		    <%=Html.Input(a => a.Address)%>
		    <%=Html.Input(a => a.City)%>
		    <%=Html.Input(a => a.Region)%>
		    <%=Html.Input(a => a.SpeakerName)%>
		    <%=Html.Input(a => a.SpeakerUrl)%>
		    <%=Html.Input(a => a.SpeakerBio).Partial("MultilineText")%>
		    
		    <p>
	        <p class="buttons">
      			<input type="submit" value="Save" />
      			<%= Html.ActionLink<HomeController>("Cancel", x=>x.Index(null)) %>
	        </p>	        			        
	    </fieldset>
    <% } %>
</asp:Content>