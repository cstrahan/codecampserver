<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage"%>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="MvcContrib"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
  <%var speakers = (SpeakerForm[])ViewData.Model; %>
  <div class="dataContainerQuadWide mt10">
	 <div class="cleaner"></div>
	 <h2>Speakers 
		<%if (User.Identity.IsAuthenticated){%>
			<a class="" href="<%=Url.Action<SpeakerController>(c=>c.New())%>" title="Add a new Speaker"><img src="/images/icons/application_add.png" /></a>
		<%}%>
	</h2>
	   <%
	   	  var counter = 0;
		  foreach (var speaker in speakers)
	   {%>
			<p class="sessionname"><a href="<%=Url.Action<SpeakerController>(t => t.Index(null), new {speaker = speaker.Id}).ToXHTMLLink()%>"><%=speaker.FirstName%> <%=speaker.LastName%></a>
			<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<SpeakerController>(c => c.Edit(null), new {speaker = speaker.Id})%>" title="View Speaker <%=counter + 1%>"><img src="/images/icons/application_edit.png"/></a>
			<%}%>
		</p>
		  <%
	   	counter++;
	   }%>
</div>
</asp:Content>