<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ConferenceForm>"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
        <div class="section-title">
            <h2><%= Model.Name%> <%Html.RenderPartial("EditConferenceLink", Model); %></h2>
            <p><%= Model.Description %></p>
            <p><%if (Model.LocationUrl.Length > 0)
                 { %>
                 <a target="_blank" href="<%=Model.LocationUrl %>"><%=Model.LocationName%></a>
            <%}else
                  {%>
            <%=Model.LocationName%></p>            
            <%
                  }%>
            <p><%= Model.GetDate() %></p>
             <%= Model.HtmlContent %>
        </div>
</asp:Content>