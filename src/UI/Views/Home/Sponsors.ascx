<%@ Control Language="C#" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewUserControl<IList<SponsorForm>>"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<div id="#sponsorlist">
<h3> Sponsors</h3>
    <ul><%foreach (SponsorForm sponsorForm in Model)
        {%>
          <li><a href="<%=sponsorForm.Url%>"><%=Html.Image(sponsorForm.BannerUrl)%></a></li>
        <%} %>
    </ul>
</div>
