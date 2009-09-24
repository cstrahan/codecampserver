<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="ViewPage<ConferenceForm[]>"%>
<%@ Import Namespace="CodeCampServer.UI.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" runat="server">
<% Html.RenderPartial("HomeMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">

    <h2>Conferences
		<%if (User.Identity.IsAuthenticated){%>
		    <%= Html.ImageLink<ConferenceController>(x=>x.New(null), "~/images/icons/application_add.png", "Add a new conference") %>
		<%}%>
    </h2>
    	
	<%Html.RenderPartial("ConferenceList", Model); %>

</asp:Content>

