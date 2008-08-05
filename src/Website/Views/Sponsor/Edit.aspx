<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
Inherits="System.Web.Mvc.ViewPage" Title="Edit Sponsor" %>
<%@ Import namespace="MvcContrib"%>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<script type="text/javascript">
function ConfirmDelete() {
    return confirm('Click OK only if you want to delete this sponsor.');
}
</script>

    <% 
        Sponsor sponsor = ViewData.Get<Sponsor>();
        string[] levels = Enum.GetNames(typeof (SponsorLevel));
        string selectedLevel = Enum.GetName(typeof (SponsorLevel), sponsor.Level);  
    %>
    
    <% using (Html.Form("sponsor", "save")) { %>    
    <fieldset>
        <legend>Edit Sponsor</legend>
        <%=Html.Hidden("oldName", sponsor.Name) %>
        <label for="displayName">Name</label>
        <%=Html.TextBox("name",sponsor.Name, 50, 50)%>
        
        <label for="level">Sponsor Level</label>
        <%= Html.Select("level", levels, new[]{ selectedLevel}) %>
        
        <label for="company">Logo Url</label>
        <%=Html.TextBox("logoUrl",sponsor.LogoUrl, 50, 260)%>
        
        <label for="website">Website</label>
        <%=Html.TextBox("website", sponsor.Website, 50, 260)%>
        
        <label for="comment">Contact First Name</label>
        <%=Html.TextBox("firstName", sponsor.Contact.FirstName, 20, 20)%>
        
        <label for="profile">Contact Last Name</label>
        <%=Html.TextBox("lastName", sponsor.Contact.LastName, 20, 20)%>
        
        <label for="profile">Contact Email</label>
        <%=Html.TextBox("email", sponsor.Contact.Email, 50, 60)%>
        
        <div class="button-row">
            <%=Html.SubmitButton("Save", "Save")%> 
        </div>        
      <% } %>
    </fieldset>
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
