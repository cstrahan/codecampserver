<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<SessionForm>"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">

    <% using (Html.BeginForm<SessionController>(x => x.Save(null, null, null))) { %>            
        <fieldset id="session">
            <legend>Edit Session</legend>
            
            <%= Errors.Display() %>
            
			<%=Html.Input(a => a.Id)%>   
			<%=Html.Input(a => a.Title)%>
			<%=Html.Input(a => a.Key)%>
			
			<label for="Abstract">Abstract:</label>
			<%=Html.TextAreaFor(a => a.Abstract, 5, 50, null)%>
			
			<%=Html.Input(a => a.Level)%>
			<%=Html.Input(a => a.Speaker)%>
			<%=Html.Input(a => a.TimeSlot)%>
			
			<%=Html.Input(a => a.Track)%>
			<%=Html.Input(a => a.RoomNumber)%>
			<%=Html.Input(a => a.MaterialsUrl)%>
			
			<%=Html.Hidden("urlreferrer", ViewData["UrlReferrer"]) %>
							
	        <p class="buttons">
			    <input type="submit" value="Save" />
			    <a href='<%= ViewData["UrlReferrer"] %>'>Cancel</a>						
		    </p>
        </fieldset>
    <% } %>
</asp:Content>