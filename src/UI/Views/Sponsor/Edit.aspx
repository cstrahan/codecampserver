<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="ViewPage<SponsorForm>"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Main" runat="server">
    <form action="<%= Url.Action<SponsorController>(x => x.Save(null,null)) %>" method="post"  >
        <div>
	        <h1>Edit Sponsor</h1>
            
            <%=Html.ValidationSummary()%>

	        <table class="dataEntry">
		        <tr>
			        <td class="w50p">
                        <%=Html.Input(a => a.ID)%>
					    <div><%=Html.Input(a => a.Name)%></div>
					    <div><%=Html.Input(a => a.Url)%></div>
					    <div><%=Html.Input(a => a.BannerUrl)%></div>
					    <div><%=Html.Input(a => a.Level)%></div>					    
			        </td>			       
		        </tr>
	        </table>
	        <br />
	        <br />
	        <div class="p10 tac">
						<%=Html.SubmitButton("save", "Save", new{@class="pr10 w100"}) %>    
						<a href="<%=Url.Action<SponsorController>(x => x.Index(null)).ToXHTMLLink() %>"  class="pr10 mt5" rel="cancel">Cancel</a>
					</div>
        </div>
    </form>
</asp:Content>