<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<SpeakerForm[]>"%>
<%@ Import Namespace="MvcContrib.UI.Grid"%>
<%@ Import Namespace="CodeCampServer.Core.Domain.Model"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
	 <h2><%=ViewData.Get<PageInfo>().SubTitle%> / Speakers 
		<%if (User.Identity.IsAuthenticated){%>
			<a class="" href="<%=Url.Action<SpeakerController>(c=>c.New())%>" title="Add a new Speaker"><img src="/images/icons/application_add.png" /></a>
		<%}%>
	</h2>
	<%=Errors.Display() %>	
	<%=Html.Grid(Model)
              .WithClass("datatable")
              .Columns(builder =>
              {
                  builder.For(
                      x =>
                      Html.RouteLink(x.FirstName + " " + x.LastName, "speaker",
                                     new {speakerKey = x.Key}))
                      .DoNotEncode()
                      .Named("Speaker");
                       builder.For(X => X.Company);
                       builder.For(X => X.JobTitle);
                       builder.For(X => X.WebsiteUrl);

                       builder.For("Edit").
                       PartialCell("EditSpeakerLink").                           
                       Visible(User.Identity.IsAuthenticated);

                       builder.For("Delete").
                       PartialCell("DeleteSpeakerLink").
                       Visible(User.Identity.IsAuthenticated);                 
                })%>	   
</asp:Content>
