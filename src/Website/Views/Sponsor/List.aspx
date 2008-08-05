<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
Inherits="System.Web.Mvc.ViewPage" Title="Sponsor List" %>
<%@ Import namespace="MvcContrib"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<ul class="sponsors">
<% foreach (Sponsor sponsor in (Sponsor[]) ViewData.Model) { %>
    <li>
        <div class="<%=sponsor.Level.ToString()%> sponsor">
            <a href='<%=sponsor.Website %>'>
                <img src='<%=sponsor.LogoUrl %>' border="0" alt='<%=sponsor.Name %> logo' />
            </a>          
            <p>
                <a href='<%=sponsor.Website %>'>
                    <%=sponsor.Name %>
                </a>
            </p>            
        </div>    
    </li>
<% } %>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
