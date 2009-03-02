<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
	Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ProposalForm>" %>
<%@ Import Namespace="CodeCampServer.Core"%>
<%@ Import Namespace="CodeCampServer.UI.Models"%>
<%@ Import Namespace="CodeCampServer.Core.Domain.Model.Planning"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>


<asp:Content ContentPlaceHolderID="Main" runat="server">
    <form action="<%=Url.Action<ProposalController>(x => x.Save(null, null))%>" method="post"  >
        <div>
	        <h1>Proposal Submission</h1>
            
          <%=Errors.Display()%>
	        <table class="dataEntry">
	        <tr><th></th></tr>
		        <tr>
			        <td class="w50p">       
								<%=Html.Input(x => x.Id)%>	
			        	<%=Html.Input(x => x.Title).Attributes(GetAttributes())%>
			        	<%=Html.Input(x => x.Status) %>
								<%=Html.Input(x => x.Level).Attributes(GetAttributes())%>
								<%=Html.Input(x => x.Track).Attributes(GetAttributes())%>
								<%=Html.Input(x => x.Abstract).Attributes(GetAttributes("5", "60"))%>
								<%=Html.Input(x => x.SubmissionDate)%>
			        </td>
		        </tr>
	        </table>
	        <br />
	        <br />
	        <div class="p10 tac">
						<%
							foreach (var command in ViewData.Get<ProposalEditInfo>().Commands)
							{
								%><%=Html.SubmitButton("command", command.TransitionVerbPresentTense, new {@class="w100 m5"}) %><%		
							} 
						%>
						<%if(!ViewData.Get<ProposalEditInfo>().ReadOnly){%>
						<a href="<%=Url.Action<HomeController>(x => x.Index()).ToXHTMLLink()%>"  class="pr10 mt5" rel="cancel">Cancel</a>				
						<%}%>
					</div>
        </div>
    </form>
</asp:Content>


<script type="text/C#" runat="server">
	private object GetAttributes()
	{
		if(ViewData.Get<ProposalEditInfo>().ReadOnly)
		{
			return new {disabled = "disabled"};
		}

		return null;
	}
	
	private object GetAttributes(string rows, string cols)
	{
		if(ViewData.Get<ProposalEditInfo>().ReadOnly)
		{
			return new {disabled = "disabled", rows=rows, cols=cols};
		}

		return new {rows=rows, cols=cols};
	}
</script>
