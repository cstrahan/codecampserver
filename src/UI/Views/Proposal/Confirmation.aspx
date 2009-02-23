<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ProposalForm>" %>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<%@ Import Namespace="CodeCampServer.Core.Domain.Messages"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">

    <h3>Proposal Has Been Submitted</h3>
		<div>
		    <strong><%=ViewData.Model.Title%> <%Html.RenderPartial("EditProposalLink", Model); %></strong>
		    
		    <div>  
						<p>Level: <%=Model.Level%> Track: <%= Model.Track.Name %> </p>
						<p>Abstract: <%=Model.Abstract%> </p>
				</div>
				<br /><br />
				<p>Click the edit icon above if you need to revise your proposal.  Click here to see 
				<a href="<%=Url.Action<ProposalController>(x=>x.List(null))%>">all proposals.</a></p>
    </div>
</asp:Content>
