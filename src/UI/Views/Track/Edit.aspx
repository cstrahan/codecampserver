<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<TrackForm>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server"></asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
        <% using(Html.BeginForm<TrackController>(x=>x.Save(null, null, null))) { %>
        
        <%= Errors.Display() %>
        <fieldset>
            <legend>Edit Track</legend>
            
            <%=Html.Input(x => x.Id) %>
		    <%=Html.Input(x => x.ConferenceId) %>
			<%=Html.Input(x => x.ConferenceKey) %>
			<%=Html.Input(a => a.Name)%>
			<%=Html.Hidden("urlreferrer", ViewData["UrlReferrer"]) %>			
            
            <p class="buttons">
            <input type="submit" value="Save" />
            <a href='<%= ViewData["UrlReferrer"] %>' rel="cancel">Cancel</a>
            </p>
        </fieldset>
        <% } %>
		
</asp:Content>
