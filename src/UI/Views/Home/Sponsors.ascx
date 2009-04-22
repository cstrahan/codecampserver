<%@ Control Language="C#" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewUserControl<IList<SponsorForm>>"%>
<%@ Import Namespace="CodeCampServer.UI"%>
<% if(Model.Count>0){ %>
 <table class="feature tac" style="width:250px;">
		<tr>
			<td><!-- Need these empty cells.  Do not remove this cell by putting colspan on featureHeading cell. --></td>
			<td class="featureHeading">Sponsors</td>
			<td><!-- Need these empty cells.  Do not remove this cell by putting colspan on featureHeading cell. --></td>
		</tr>
		<tr>
			<td class="featureLeftEndCap"></td>
			<td class="featureContent">
			<div id="#sponsorlist" class="tac">
            <%foreach (SponsorForm sponsorForm in Model)
                {%>
                  <div><%=sponsorForm.Name %></div><div><a href="<%=sponsorForm.Url%>"><%=Html.Image(sponsorForm.BannerUrl)%></a></div>
                <%} %>
            
            </div>
			</td>
			<td class="featureRightEndCap"></td>
		</tr>
  </table>
  <%} %>