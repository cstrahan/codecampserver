<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ConferenceForm[]>"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" runat="server">
<% Html.RenderPartial("HomeMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <%var conferences = Model; %>
  <div class="dataContainerQuadWide mt10">
  <h2>Conferences
		<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<ConferenceController>(c=>c.New(null))%>" title="Add a new Conference"><img src="/images/icons/application_add.png" /></a>
		<%}%>
</h2>
	 <div class="cleaner"></div>
	<%Html.RenderPartial("ConferenceList", Model); %>
</div>
</asp:Content>

