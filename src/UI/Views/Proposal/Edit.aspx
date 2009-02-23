<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
	Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ProposalForm>" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <form action="<%=Url.Action<ProposalController>(x => x.Save(null))%>" method="post"  >
        <div>
	        <h1>Submittal Proposal</h1>
            
          <%=Errors.Display()%>
	        <table class="dataEntry">
	        <tr><th></th></tr>
		        <tr>
			        <td class="w50p">       	
			        	<%=InputFor(x => x.Title)%>
								<%=InputFor(x => x.Level)%>
								<%=InputFor(x => x.Track)%>
								<%=InputFor(x => x.Abstract).Attributes(new {rows="5", cols="60"})%>
			        </td>
		        </tr>
	        </table>
	        <br />
	        <br />
	        <div class="p10 tac">
						<%=Html.SubmitButton("save", "Submit Proposal", new {@class = "pr10 w120"})%>    
						<a href="<%=Url.Action<HomeController>(x => x.Index()).ToXHTMLLink()%>"  class="pr10 mt5" rel="cancel">Cancel</a>				
					</div>
        </div>
    </form>
</asp:Content>