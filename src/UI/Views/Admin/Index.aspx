<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" Inherits="CodeCampServer.UI.ViewPage.BaseViewPage"%>

<%@ Import Namespace="System.Security.Policy"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <div class="h400">
  <h1>Administration</h1>  
  <ul>
      <li><a href="<%=Url.Action("editadminpassword") %>"> Update Admin Password</a></li>
      <li>Update Conference Information</li>
      <li>Update Conference Tracks</li>
      <li>Update Conference Timeslots</li>
      <li>Update Conference Sessions</li>
  </ul>
  </div>
</asp:Content>