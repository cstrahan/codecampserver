<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="MvcContrib"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
  <%var speaker = (SpeakerForm)ViewData.Model; %>
  <div class="dataContainerQuadWide mt10">
	 <div class="cleaner"></div>
		<strong><%= speaker.FirstName %> <%= speaker.LastName %></strong>, <%=speaker.JobTitle %><br />
		<%=speaker.Company %><br />
		<%=speaker.WebsiteUrl %><br />
		<%= speaker.Bio%>
</div>
</asp:Content>