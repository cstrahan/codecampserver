<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
	Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ProposalForm>" %>


<asp:Content ContentPlaceHolderID="Main" runat="server">
    <form action="<%=Url.Action<ProposalController>(x => x.Save(null, null))%>" method="post"  >
        <div>
	        <h1>Proposal Submission</h1>
            
          <%=Errors.Display()%>
	        <table class="dataEntry">
	        <tr><th></th></tr>
		        <tr>
			        <td class="w50p">       
								<%=Html.Hidden(x => x.Id)%>	
			        	<%=Html.Input(x => x.Title, GetAttributes())%>
			        	<%=Html.Display(x => x.Status) %>
								<%=Html.Input(x => x.Level, GetAttributes())%>
								<%=Html.Input(x => x.Track, GetAttributes())%>
								<%=Html.Input(x => x.Abstract, GetAttributes("5", "60"))%>
								<%=Html.Display(x => x.SubmissionDate)%>
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
						<a href="<%=Url.Action<HomeController>(x => x.Index(null,null)).ToXHTMLLink()%>"  class="pr10 mt5" rel="cancel">Cancel</a>				
						<%}%>
					</div>
        </div>
    </form>
</asp:Content>


<script type="text/C#" runat="server">
	private IDictionary<string, object> GetAttributes()
	{
		if(ViewData.Get<ProposalEditInfo>().ReadOnly)
		{
			return new Dictionary<string, object>{{"disabled", "disabled"}};
		}

		return null;
	}
	
	private IDictionary<string, object> GetAttributes(string rows, string cols)
	{
		if(ViewData.Get<ProposalEditInfo>().ReadOnly)
		{
			return new Dictionary<string, object>{{"disabled", "disabled"}, {"rows", rows}, {"cols", cols}};
		}

		return new Dictionary<string, object>{{"rows", rows}, {"cols", cols}};
	}
</script>
