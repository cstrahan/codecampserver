<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ProposalForm[]>" %>

<%@ Import Namespace="CodeCampServer.UI"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
	<h2>Session Proposals
		<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<ProposalController>(c=>c.New(null))%>" title="Make a new proposal"><img src="/images/icons/application_add.png" /></a>
		<%}%>
	</h2>

	<% foreach (var proposal in ViewData.Model) { %>	
		<div>
			<strong><%=proposal.Title%> <i>(<%=proposal.Status%>)</i> <%Html.RenderPartial("EditProposalLink", proposal); %></strong>
			<div>  
					<p>Level: <%=proposal.Level%> Track: <%= proposal.Track.Name %> </p>
					<p>Abstract: <%=proposal.Abstract%> </p>
			</div>
		</div>			
	<% } %>
</asp:Content>

