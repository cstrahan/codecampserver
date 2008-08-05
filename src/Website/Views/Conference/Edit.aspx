<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Admin.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" Title="Code Camp Server" %>
<%@ Import Namespace="System.Collections.Generic"%>
<%@ Import Namespace="CodeCampServer.Website.Controllers"%>
<%@ Import namespace="MvcContrib"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
  <script type="text/javascript">
    $(document).ready( function() {
      $('#conf_start').datepicker();
      $('#conf_end').datepicker();
      
      $('#conf_save').click( function() {
        var start = $('#conf_start').value;
      });
      
      jQuery.validator.addMethod("uniqueKey", function(value, element) { alert(value); alert(element.id); return checkKey(value); }, "This URL Key has already been used");      
      $("form#new_conference").validate({
        errorContainer : '#errors',
        errorClass: "formError",
        messages: {
          conf_name: "Required",
          conf_key: "Required",
          conf_start: "Required",
          conf_end: "Required",
          conf_descr: "Required"
        }        
      });           
      
    });
    
    function checkKey(key) {
      $.ajax({
        async: false,
        method: 'GET',
        url: '/conference/keycheck/' + key, //replace with Url.Action() with a correct route
        dataType:"json",
        beforeSend: function() { $("#indicator").show(); },
        complete: function() {  $("#indicator").hide(); },
        success: function(result) {
          return result;              
        }
      });
    }
    
  </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">    
            
      <p><%= Html.ActionLink<ConferenceController>(c => c.List(), "« Cancel") %></p>
      
      <div id="errors"></div>
            
      <form method="post" id="new_conference" action='<%= Url.Action("save") %>'>
        <fieldset>
            <legend>New Conference</legend>
            
            <label for="conf_name">Name</label>
            <% var  conference = ViewData.Get<Conference>();%>
            <%= Html.TextBox("conf_name", conference.Name ?? "", 40, 100, new { @class="required"}) %>
            <span class="info">The name of the conference.</span>
            
            <label for="conf_key">Unique Key</label>
            <%= Html.TextBox("conf_key", conference.Key ?? "", 25, 25, new { @class="required uniqueKey" })%>
            <span class="info">A unique name to identify the conference.  Will be used in a url, so it must not contain illegal characters such as spaces or symbols.</span>
            
            <label for="conf_start">Starts</label>        
            <%= Html.TextBox("conf_start", conference.StartDate.HasValue ? 
                          conference.StartDate.Value.ToShortDateString() : "",
                          25, 10, new { @class = "required" })%>
            
            <label for="conf_end">Ends</label>        
            <%= Html.TextBox("conf_end", conference.EndDate.HasValue ? 
              conference.EndDate.Value.ToShortDateString() : "",
                                        25, 10, new { @class = "required" })%>
            
            <label for="conf_descr">Description</label>
            <%= Html.TextArea("conf_descr", conference.Description ?? "", 7, 60, new { @class = "required" })%>
            <span class="info">Max 1000 characters.  No formatting.</span>
            
            <label for="conf_enabled">Visible to the public</label>
            <%= Html.CheckBox("conf_enabled", "", conference.PubliclyVisible) %>
            
            <div class="button-row">
                <input type="submit" id="conf_save" value="Save" class="submit" />
            </span>
        </fieldset>    
    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
