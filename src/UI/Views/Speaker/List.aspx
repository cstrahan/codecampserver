<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<SpeakerForm[]>"%>
<%@ Import Namespace="CodeCampServer.UI.Helpers"%>
<%@ Import Namespace="MvcContrib.UI.Grid"%>
<%@ Import Namespace="CodeCampServer.Core.Domain.Model"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
	 <h2><%=ViewData.Get<PageInfo>().SubTitle%> / Speakers 
		<%if (User.Identity.IsAuthenticated) { %>
		    <%= Html.ImageLink<SpeakerController>(c=>c.New(), "~/images/icons/application_add.png", "Add a new speaker") %>			
		<%}%>
	</h2>
	<%=Errors.Display() %>	
	<%=Html.Grid(Model)
              .WithClass("default datatable")
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
