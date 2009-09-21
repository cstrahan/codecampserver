<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
	Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ProposalForm>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <% using(Html.BeginForm<ProposalController>(x=>x.Save(null, null))) { %>    
    
        <fieldset>
            <legend>Proposal Submissions</legend>        
	                        
            <%=Errors.Display()%>
	        
	        <%=Html.Hidden(x => x.Id)%>	
			<%=Html.Input(x => x.Title)%>
			<%=Html.Display(x => x.Status) %>
			<%=Html.Input(x => x.Level)%>
			<%=Html.Input(x => x.Track)%>
						
			<label for="Abstract">Abstract</label>
			<%=Html.TextAreaFor(x=>x.Abstract, 7, 35, null) %>
			
			<%=Html.Display(x => x.SubmissionDate)%>
			
			<p class="buttons">	
			<%foreach (var command in ViewData.Get<ProposalEditInfo>().Commands) { %>
			    <%=Html.SubmitButton("command", command.TransitionVerbPresentTense) %>
			<% } %>
				
			<%if(!ViewData.Get<ProposalEditInfo>().ReadOnly) { %>
			    <%= Html.ActionLink<HomeController>("Cancel", x=>x.Index(null)) %>					
			<% } %>
			</p>
			
			</fieldset>        
    <% } %>
    
    <% if(ViewData.Get<ProposalEditInfo>().ReadOnly) { %>
    <script type="text/javascript">
        $("fieldset input, fieldset select, fieldset textarea").attr("disabled", "disabled");
    </script>
    <% } %>
    
</asp:Content>