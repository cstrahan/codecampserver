<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.Website.Views.ViewBase" Title="Sponsor List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<ul class="sponsors">
<% foreach (ConfirmedSponsor sponsor in ViewData.Get<ConfirmedSponsor[]>()) { %>
    <li>
        <div class="<%=sponsor.Level.ToString()%> sponsor">
            <a href='<%=sponsor.Sponsor.Website %>'>
                <img src='<%=sponsor.Sponsor.LogoUrl %>' border="0" alt='<%=sponsor.Sponsor.Name %> logo' />
            </a>          
            <p>
                <a href='<%=sponsor.Sponsor.Website %>'>
                    <%=sponsor.Sponsor.Name %>
                </a>
            </p>            
        </div>    
    </li>
<% } %>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
