<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"  validateRequest="false"
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ConferenceForm>"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
<script type="text/javascript" src="/scripts/tiny_mce/tiny_mce.js"></script>
<script type="text/javascript">
    tinyMCE.init({
        mode: "textareas",
        theme: "advanced",
        theme_advanced_buttons1: "justifyleft,justifycenter,justifyright,bold,italic,bullist,numlist,indent,outdent,fontsizeselect,forecolor,fontselect,code",
        theme_advanced_buttons2: "",        
        plugins : "style",
	    theme_advanced_buttons1_add : "styleprops",
	    verify_html: false
    });
</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">

    <% using(Html.BeginForm<ConferenceController>(x=>x.Save(null))) { %>
        <fieldset id="conference">
            <legend>Edit Conference</legend>    
            
            <%=Errors.Display()%>

			<%=Html.Input(a => a.Id)%>            	
		    <%=Html.Input(a => a.UserGroupId)%>
		    <%=Html.Input(a => a.Name)%>
		    <%=Html.Input(a => a.Key)%>
		    <%=Html.Input(a => a.Description)%>
		    <%=Html.Input(a => a.StartDate)%>
		    <%=Html.Input(a => a.EndDate)%>
		    <%=Html.Input(a => a.TimeZone)%>
		    <%=Html.Input(a => a.HasRegistration)%>
		    <%=Html.Input(a => a.LocationName)%>
		    <%=Html.Input(a => a.LocationUrl)%>
		    <%=Html.Input(a => a.Address)%>
		    <%=Html.Input(a => a.City)%>
		    <%=Html.Input(a => a.Region)%>
		    <%=Html.Input(a => a.PostalCode)%>
		    <%=Html.Input(a => a.PhoneNumber)%>
		    
		    <p>
		    <label for="HtmlContent">Home Page Content</label>
		    <%= Html.TextAreaFor(a=>a.HtmlContent) %>		    
		    </p>
		        	           
	        <p class="buttons">
      			<input type="submit" value="Save" />
      			<%= Html.ActionLink<ConferenceController>("Cancel", x=>x.Index(null)) %>				
	        </p>	        			        
	    </fieldset>
    <% } %>
</asp:Content>