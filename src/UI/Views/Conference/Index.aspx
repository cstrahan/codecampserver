<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.ConferenceEditView"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<%@ Import Namespace="CodeCampServer.UI"%>

<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
        <div>
            <h2><%= Model.Description %></h2>
            <p><%= Model.LocationName %></p>
            <p><%= Model.StartDate %>&nbsp;to&nbsp;<%= Model.EndDate %></p>
			  
        </div>
    </form>
</asp:Content>