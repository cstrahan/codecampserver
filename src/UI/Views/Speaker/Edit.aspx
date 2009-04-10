<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<SpeakerForm>"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Main" runat="server">
    <form action="<%= Url.Action<SpeakerController>(x => x.Save(null,null)) %>" method="post"  >
        <div>
	        <h1>Edit Speaker</h1>
            
            <%=Errors.Display()%>

	        <table class="dataEntry">
		        <tr>
			        <td class="w50p">
									<%=Html.Input(a => a.Id)%>            	
					    <div><%=Html.Input(a => a.FirstName)%></div>
					    <div><%=Html.Input(a => a.LastName)%></div>
					    <div><%=Html.Input(a => a.Key)%></div>
					    <div><%=Html.Input(a => a.EmailAddress)%></div>
					    <div><%=Html.Input(a => a.Company)%></div>
					    <div><%=Html.Input(a => a.JobTitle)%></div>
					    <div><%=Html.Input(a => a.WebsiteUrl)%></div>
					    <div><%=Html.Input(a => a.Bio)%></div>
			        </td>
			       
		        </tr>
	        </table>
	        <br />
	        <br />
	        <div class="p10 tac">
						<%=Html.SubmitButton("save", "Save", new{@class="pr10 w100"}) %>    
						<a href="<%=Url.Action<AdminController>(x => x.Index(null)).ToXHTMLLink() %>"  class="pr10 mt5" rel="cancel">Cancel</a>				
					</div>
        </div>
    </form>
</asp:Content>