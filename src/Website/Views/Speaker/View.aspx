<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" 
    Inherits="CodeCampServer.Website.Views.ViewBase" Title="Speaker Details" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <% Speaker _speaker = ViewData.Get<Speaker>(); %>
    Name:    <%=_speaker.Contact.FullName%><br />
    Email:   <%=_speaker.Contact.Email%><br />
    Website: <%=_speaker.Website%><br />
    Profile: <%=_speaker.Profile%><br />
    Comment: <%=_speaker.Comment%><br />
</asp:Content>
