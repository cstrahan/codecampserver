<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
    Inherits="System.Web.Mvc.ViewPage" Title="Speaker Details" %>
<%@ Import namespace="MvcContrib"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <% Speaker speaker = ViewData.Get<Speaker>(); %>
    Name:    <%=speaker.Person.Contact.FullName%><br />
    Email:   <%=speaker.Person.Contact.Email%><br />
    Website: <%=speaker.Person.Website%><br />
    Profile: <%=speaker.Bio%><br />
    Comment: <%=speaker.Person.Comment%><br />
</asp:Content>
