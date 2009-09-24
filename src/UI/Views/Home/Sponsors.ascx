<%@ Control Language="C#" AutoEventWireup="true" Inherits="ViewUserControl<IList<SponsorForm>>"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<% if(Model.Count>0){ %>
<ul> 
	<li> 
		<h2>Sponsors</h2> 
		<ul> 
		<%foreach (SponsorForm sponsorForm in Model){%>
			<li><a href="<%=sponsorForm.Url%>"><%=sponsorForm.Name%></a></li> 
		<%}%>
		</ul> 
	</li> 
</ul>
<%}%>