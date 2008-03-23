<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
    Inherits="System.Web.Mvc.ViewPage" Title="Proposed Sessions" %>
<%@ Import namespace="CodeCampServer.Website.Views"%>
<%@ Import Namespace="CodeCampServer.Model.Domain" %>
<%@ Import Namespace="CodeCampServer.Model.Presentation" %>
<%@ Import Namespace="System.Collections.Generic" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h3>Proposed Sessions for <%=ViewData.Get<Conference>().Name%></h3>

<% 
    List<Session> sessions = new List<Session>(ViewData.Get<IEnumerable<Session>>());
    if (sessions.Count == 0)
    { %>
    There are no sessions waiting for approval!
<%  }
    else 
    { %>
    <ul>
    <% foreach (Session session in ViewData.Get<IEnumerable<Session>>())
       { %>
        <li>
            <span class="session-name"><%=session.Title%></span>        
        </li>
    <% } %>
    </ul>
<% } %>
</asp:Content>