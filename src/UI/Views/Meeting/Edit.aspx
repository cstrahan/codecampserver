<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"  validateRequest="false"
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<MeetingForm>"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">

    <% using(Html.BeginForm<MeetingController>(x=>x.Save(null))) { %>
        <fieldset id="meeting">
            <legend>Edit Meeting</legend>    
            
            <%=Errors.Display()%>
		    <%
    var rows = new Dictionary<string, object>();
    rows.Add("rows",10); %>

			<%=Html.Input(a => a.Id)%>            	
		    <%=Html.Input(a => a.UserGroupId)%>
		    <%=Html.Input(a => a.Name)%>
		    <%=Html.Input(a => a.Key)%>
		    <%=Html.Input(a => a.Description)%>
		    <%=Html.Input(a => a.StartDate)%>
		    <%=Html.Input(a => a.EndDate)%>
		    <%=Html.Input(a => a.TimeZone)%>
		    <%=Html.Input(a => a.LocationName)%>
		    <%=Html.Input(a => a.LocationUrl)%>
		    <%=Html.Input(a => a.Address)%>
		    <%=Html.Input(a => a.City)%>
		    <%=Html.Input(a => a.Region)%>
		    <%=Html.Input(a => a.SpeakerName)%>
		    <%=Html.Input(a => a.SpeakerUrl)%>
		    <%=Html.Input(a => a.SpeakerBio).Attributes(rows)%>
		    <%=Html.Input(a => a.Topic).Attributes(rows)%>
		    <%=Html.Input(a => a.Summary).Attributes(rows)%>
		    
		    <p>
	        <p class="buttons">
      			<input type="submit" value="Save" />
      			<%= Html.ActionLink<EventController>("Cancel", x=>x.List(null)) %>
	        </p>	        			        
	    </fieldset>
    <% } %>
</asp:Content>