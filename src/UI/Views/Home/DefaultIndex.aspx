<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
    Inherits="ViewPage<UserGroupInput>" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
    <script type="text/javascript" src="/scripts/rsswidget.js"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="Main" runat="server">
<%Html.RenderAction("allupcomingevents", "event"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="SidebarPlaceHolder" runat="server">
<h2 class="sidebarTitle">What is CodeCampServer.com?</h2>
<p class="sidebarCopy">CodeCampServer.com is a hosted website that supports the 
management of .Net user group events.  Just point your user group domain name’s 
(e.i.www.adnug.org) DNS to us, and we’ll do the rest to host your group’s website.
  Manage monthly meetings as well as Code Camps through the built-in administrative
   interface.  Obtain free website hosting through a community partnership between 
   Headspring Systems and ORCSWeb.  Join the discussion list in order to discuss new
    features or to move your user group onto the platform:
     <a href="http://groups.google.com/group/codecampserver-discuss">codecampserver-discuss</a></p>

<h2 class="sidebarTitle">What is CodeCampServer?</h2>
<p class="sidebarCopy">CodeCampServer is an OSS (open source software) application 
with a multi-tenant architecture.  Using ASP.NET MVC and URL routing based on domain
 name; a single application instance can host many .Net user group websites.  The feature
  set of CodeCampServer is constantly being enhanced by the .Net community.  You will 
  find the project site at <a href="http://codecampserver.org">http://codecampserver.org</a></p>
</asp:Content>
