<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
    Inherits="System.Web.Mvc.ViewPage" Title="Edit Speaker" %>
<%@ Import namespace="CodeCampServer.Website.Views"%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <% Speaker speaker = ViewData.Get<Speaker>(); %>
    <% using (Html.Form("save", "speaker")) { %>    
    <fieldset>
        <legend>Edit Speaker Profile</legend>
        
        <label for="displayName">Display Name</label>
        <%=Html.TextBox("displayName",speaker.DisplayName)%>
        
        <label for="firstName">First Name</label>
        <%=Html.TextBox("firstName",speaker.Contact.FirstName)%>
        
        <label for="lastName">Last Name</label>
        <%=Html.TextBox("lastName", speaker.Contact.LastName)%>
        
        <label for="website">Website</label>
        <%=Html.TextBox("website", speaker.Website, 80)%>
        
        <label for="comment">Comment</label>
        <%=Html.TextArea("comment", speaker.Comment, 4, 79)%>
        
        <label for="profile">Profile</label>
        <%=Html.TextArea("profile", speaker.Profile, 4, 79)%>
        
        <div class="button-row">
            <%=Html.SubmitButton("Save", "Save")%>
        </div>
    </fieldset>
    <% } %>
</asp:Content>
