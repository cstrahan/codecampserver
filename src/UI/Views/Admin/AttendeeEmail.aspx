<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" runat="server">
<% Html.RenderPartial("HomeMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <div class="h100">
	  <h1>Attendee Email Addresses</h1>  
	    <textarea id="emailaddresses" rows="30" cols="50"><%
	            foreach (var attendee in ViewData.Get<Attendee[]>())
	            {
	                Response.Write(attendee.EmailAddress+",\n");
	            } %></textarea>
	  
  </div>
</asp:Content>
