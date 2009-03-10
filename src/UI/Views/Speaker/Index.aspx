<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<SpeakerForm>"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <%var speaker = Model; %>
  <div class="dataContainerQuadWide mt10">
	 <div class="cleaner"></div>
		<strong><%= speaker.FirstName %> <%= speaker.LastName %></strong>, <%=speaker.JobTitle %> 
		<%Html.RenderPartial("EditSpeakerLink",speaker); %><br />
		<%=speaker.Company %><br />
		<%=speaker.WebsiteUrl %><br />
		<%= speaker.Bio%>
</div>
</asp:Content>