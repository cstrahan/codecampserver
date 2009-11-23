<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<SponsorInput>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
<a href="<%=Url.Action<SponsorController>(t=>t.Edit(null,(SponsorInput)null), new{sponsorID = Model.ID}) %>"><img src="/images/icons/application_edit.png" /></a>
<%}%>