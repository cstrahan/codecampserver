<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="CodeCampServer.Website.Components.SponsorComponent.Views.List" %>

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