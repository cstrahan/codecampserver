<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ProposalForm[]>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">

    <script src="../../scripts/ProposalList.js" type="text/javascript"></script>
	<h2>Session Proposals
		<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<ProposalController>(c=>c.New(null))%>" title="Make a new proposal"><img src="/images/icons/application_add.png" /></a>
		<%}%>
		
	</h2>

	<% foreach (var proposal in ViewData.Model) { %>	
		
		<div class="proposal">
		    
		    <div class="vote-wrapper">
		        <a href="<%= Url.Action<ProposalController>(c=>c.Vote(Guid.Empty),new{id = proposal.Id}) %>" class="vote" alt="Vote for this Session">Vote</a>
		    </div>    
			
			<strong><%=proposal.Title%> <i>(<%=proposal.Status%>)</i> <%Html.RenderPartial("EditProposalLink", proposal); %></strong>
			<div>  
					<p>Level: <%=proposal.Level%> Track: <%= proposal.Track.Name %> </p>
					<p class="proposal-abstract">Abstract: <%=proposal.Abstract%> </p>
			</div>
		</div>			
	<% } %>
</asp:Content>

