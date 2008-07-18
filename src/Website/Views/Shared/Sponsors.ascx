<%@ Import Namespace="MvcContrib"%>
<%@ Import Namespace="System.Collections.Generic"%>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="CodeCampServer.Website.Views.Shared.Sponsors" %>

<ul>
    <li>
    <h2>Platinum Sponsors</h2>
        <ul>

        <% foreach (var sponsor in ViewData.Model) { %>
        <li>

            <a href='<%= sponsor.Website %>'>
                <img alt='<%=sponsor.Name %> logo' height="100px" width="100px" src='<%= sponsor.LogoUrl %>' /><br />
                <%=sponsor.Name %>
            </a>
    
        </li>
        <% } %>

        </ul>
    </li>
</ul>