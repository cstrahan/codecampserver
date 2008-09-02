<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="MvcContrib"%>
<%@ Import Namespace="System.Collections.Generic"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<ul>
<li>
<h2>Platinum Sponsors</h2>
<ul>

<% foreach (var sponsor in (IEnumerable<Sponsor>) ViewData.Model) { %>

<li>

<a href='<%= sponsor.Website %>'>
    <img src='<%= sponsor.LogoUrl %>' alt='<%= sponsor.Name %>' />    
    <%=sponsor.Name %>
</a>

</li>

<% } %>

</ul>
</li>
</ul>