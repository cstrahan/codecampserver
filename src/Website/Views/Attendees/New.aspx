<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" Title="Register for conference" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import namespace="MvcContrib"%>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>

<asp:Content ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div>
		<h3></h3>
		<% using (Html.Form("attendees", "create")) { %>    
			
		<fieldset>
		    <legend>Register for <%=getConference().Name %></legend>
		
            <label for="firstName">First Name</label>
            <%=Html.TextBox("firstName", 30)%>
		    
            <label for="lastName">Last Name</label>
            <%=Html.TextBox("lastName", 30)%>
		    
            <label for="email">Email Address:</label>
            <%=Html.TextBox("email", 30)%>
		    
            <label for="password">Password</label>
            <%=Html.Password("password", 30)%>
		    
            <label for="website">Website URL:</label>
            <%=Html.TextBox("website", 80)%>
		    
            <label for="comment">Comment</label>
            <%=Html.TextArea("comment", "", 4, 79, new{})%>
            
            <div class="button-row">
                <%=Html.SubmitButton("register", "Register Me!")%>
            </div>
		    
		</fieldset>
		<% } %>
	</div>
</asp:Content>

<asp:Content ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">

</asp:Content>

<script runat="server">
	private Schedule getConference()
	{
		Schedule conference = ViewData.Get<Schedule>();
		return conference;
	}

</script>
