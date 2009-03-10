<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<SpeakerForm[]>"%>
<%@ Import Namespace="CodeCampServer.Core.Domain.Model"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <%var speakers = Model; %>
	 <h2>Speakers 
		<%if (User.Identity.IsAuthenticated){%>
			<a class="" href="<%=Url.Action<SpeakerController>(c=>c.New())%>" title="Add a new Speaker"><img src="/images/icons/application_add.png" /></a>
		<%}%>
	</h2>
	<%=Errors.Display() %>
	   <%
	   	  var counter = 0;
		  foreach (var speaker in speakers)
			  
	   {%>
	   <div class=" w250 ">
			<div class="fl"><a href="/<%=(ViewData.Get<Conference>().Key+"/speakers/"+speaker.Key).ToXHTMLLink()%>"><%=speaker.FirstName%> <%=speaker.LastName%></a></div>
			<div class="fr pr15">
			<%Html.RenderPartial("DeleteSpeakerLink",speaker); %>			
			</div>
			<div class="fr"><%Html.RenderPartial("EditSpeakerLink",speaker); %></div>
			<div class="cleaner"></div>
		</div>
		  <%counter++;}%>
</asp:Content>
