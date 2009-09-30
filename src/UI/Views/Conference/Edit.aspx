<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
  ValidateRequest="false" Inherits="ViewPage<ConferenceInput>" %>

<%@ Import Namespace="MvcContrib.UI.InputBuilder" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <fieldset id="conference">
    <h3>Edit Conference</h3>
    <%=Html.InputForm() %>
    <%= Html.ActionLink<HomeController>("Cancel", x=>x.Index(null)) %>    
  </fieldset>
</asp:Content>
