<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="ViewPage<UserGroupForm>"%>
<%@ Import Namespace="Microsoft.Web.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
<script type="text/javascript" src="/scripts/tiny_mce/tiny_mce.js"></script>
<script type="text/javascript">
    tinyMCE.init({
        mode: "textareas",
        theme: "advanced",
        theme_advanced_buttons1: "justifyleft,justifycenter,justifyright,bold,italic,bullist,numlist,indent,outdent,fontsizeselect,forecolor,fontselect,code",
        theme_advanced_buttons2: "",
        plugins: "style",
        theme_advanced_buttons1_add: "styleprops",
        verify_html: false
    });
</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <% using(Html.BeginForm<UserGroupController>(x=>x.Save(null))) { %>
    <%= Html.ValidationSummary() %>
    
    
    <fieldset class="wide">
        <h3>Edit User Group</h3>
        
        <%=Html.Input(a => a.Key)%>
        <%=Html.Input(a => a.DomainName)%>
        <%=Html.Input(a => a.Id)%>
        <%=Html.Input(a => a.Name)%>
        <%=Html.Input(a => a.City)%>
        <%=Html.Input(a => a.Region)%>
        <%=Html.Input(a => a.Country)%>
        <%=Html.Input(a => a.Users)%>
        <%=Html.Input(a => a.HomepageHTML).Partial("MultilineText")%>
        <%=Html.Input(a => a.GoogleAnalysticsCode)%>        
        
        <p class="buttons">
            <input type="submit" value="Save" />
            <%= Html.ActionLink<UserGroupController>(x=>x.List(), "Cancel") %>				
        </p>
    </fieldset>
    <% } %>
</asp:Content>
