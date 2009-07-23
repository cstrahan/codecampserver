<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<TimeSlotForm>"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <% using(Html.BeginForm<TimeSlotController>(c=>c.Save(null, null, null))) { %>
    <%= Errors.Display() %>
    
    <fieldset>
        <legend>Edit Time Slot</legend>
        
        <%=Html.Input(a => a.Id)%>
        <%=Html.Input(a => a.ConferenceId)%>          	
        <%=Html.Input(a => a.ConferenceKey)%> 
		<%=Html.Input(a => a.StartTime)%>
		<%=Html.Input(a => a.EndTime)%>
		<%= Html.Hidden("urlreferrer", ViewData["UrlReferrer"]) %>		
		
		<p class="buttons">
		    <input type="submit" value="Save" />
		    <a href='<%= ViewData["UrlReferrer"] %>' rel="cancel">Cancel</a>
		</p>
    </fieldset>
    <% } %>
</asp:Content>