<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
	Inherits="CodeCampServer.Website.Views.ViewBase" Title="CodeCampServer - New Session" %>
    
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div>
		<% using (Html.Form("CreateNew", "session")) { %>
		
		<fieldset>
		    
		    <legend>Create New Session</legend>
		    
		    <%=Html.Hidden("speakerEmail", ViewData.Get<Speaker>().Contact.Email) %>
		
            <label for="title">Title</label>
            <%=Html.TextBox("title", 80)%>     

            <label for="abstract">Abstract</label>
            <%=Html.TextArea("abstract", "", 5, 79)%>
            <span class="info">Please provide an overview of the session.</span>

            <label for="blogName">Blog Name</label>
            <%=Html.TextBox("blogName", 80)%>     

            <label for="blogUrl">Blog Url</label>
            <%=Html.TextBox("blogUrl", 80)%>     

            <label for="websiteName">Website Name</label>
            <%=Html.TextBox("websiteName", 80)%>     

            <label for="websiteUrl">Website Url</label>
            <%=Html.TextBox("websiteUrl", 80)%>     

            <label for="downloadName">Session Download Name</label>
            <%=Html.TextBox("downloadName", 80)%>     

            <label for="downloadUrl">Session Download Url</label>
            <%=Html.TextBox("downloadUrl", 80)%>   
            
            <div class="button-row">
                <%=Html.SubmitButton("Register", "Register Session")%>
            </div>  

		</fieldset>
		<% } %>
    </div>
</asp:Content>