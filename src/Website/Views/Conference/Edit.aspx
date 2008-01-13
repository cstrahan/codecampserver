<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="CodeCampServer.Website.Views.Conference.Edit" Title="Untitled Page" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.BindingHelpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">    
      
    <form method="post" action='<%= Url.Action("save") %>'>    
    
        <fieldset>
            <legend>New Conference</legend>                       
            
            <label for="conf_name">Name</label>
            <%= Html.TextBox("conf_name", ViewData.Name ?? "") %>            
            <span class="info">The name of the conference.</span>
            
            <label for="conf_key">Unique Key</label>
            <%= Html.TextBox("conf_key", ViewData.Key ?? "") %>           
            <span class="info">A unique name to identify the conference.  Will be used in a url, so it must not contain illegal characters such as spaces or symbols.</span>
            
            <label for="conf_start">Starts</label>        
            <%= Html.TextBox("conf_start", ViewData.StartDate.HasValue? ViewData.StartDate.Value.ToShortDateString() : "") %>
            
            <label for="conf_end">Ends</label>        
            <%= Html.TextBox("conf_end", ViewData.EndDate.HasValue? ViewData.EndDate.Value.ToShortDateString() : "") %>
            
            <label for="conf_descr">Description</label>
            <%= Html.TextArea("conf_descr", ViewData.Description ?? "", 1000, 6, 50) %>            
            <span class="info">Max 1000 characters.  No formatting.</span>
            
            <span class="button-row">
                <input type="submit" id="conf_save" value="Save" />
            </span>
        </fieldset>    
    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
