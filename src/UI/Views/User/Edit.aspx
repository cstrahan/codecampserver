<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<UserForm>"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">

<% using(Html.BeginForm("Save")) { %>
        <%=Errors.Display()%>
        
        <fieldset>
            <legend>User Profile</legend>
        
            <%=Html.Input(a => a.Id)%>            	
			<%=Html.Input(a => a.Username)%>
			<%=Html.Input(a => a.Name)%>
			<%=Html.Input(a => a.EmailAddress)%>
			<%=Html.Input(a => a.Password)%>
			<%=Html.Input(a => a.ConfirmPassword)%>
			
			<p class="buttons">
			    <input type="submit" value="Save" />
			    <%= Html.ActionLink<AdminController>("Cancel", x=>x.Index(null)) %>				
			</p>
		</fieldset>			

<% } %>

</asp:Content>