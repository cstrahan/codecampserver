<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="ViewPage<SponsorInput>"%>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Main" runat="server">
  <fieldset id="sponsor">
    <h3>Edit Sponsor</h3>
    <%=Html.InputForm() %>
    <%= Html.ActionLink<SponsorController>("Cancel", x=>x.Index(null)) %>
  </fieldset>

</asp:Content>