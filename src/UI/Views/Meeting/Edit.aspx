<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
  ValidateRequest="false" Inherits="ViewPage<MeetingForm>" %>
<%@ Import Namespace="MvcContrib.UI.InputBuilder" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <fieldset id="meeting">
    <h3>Edit Meeting</h3>
    <%=Html.InputForm() %>
    <%= Html.ActionLink<HomeController>("Cancel", x=>x.Index(null)) %>
  </fieldset>
</asp:Content>
