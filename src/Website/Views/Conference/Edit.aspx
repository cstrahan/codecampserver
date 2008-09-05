<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Admin.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.Website.Views.Conference.Edit" Title="Code Camp Server" %>
<%@ Import Namespace="System.Collections.Generic"%>
<%@ Import Namespace="CodeCampServer.Website.Controllers"%>
<%@ Import namespace="MvcContrib"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
  <script type="text/javascript">
    $(document).ready( function() {
      $('#StartDate').datepicker();
      $('#EndDate').datepicker();
      
      $('#conf_save').click( function() {
        var start = $('#StartDate').value;
      });
      
      jQuery.validator.addMethod("uniqueKey", function(value, element) {             
            return !checkKey(value);
        }, "This URL Key has already been used");      
        
      $("form#new_conference").validate({
        errorContainer : '#errors',
        errorClass: "formError",
        messages: {
          Name: "Required",
          Key: "Required/Must be unique",
          StartDate: "Required",
          EndDate: "Required",
          Description: "Required"
        }        
      });           
      
    });
    
    function checkKey(key) {        
        $.ajax({
            async: false,
            method: 'GET',
            url: '<%= Url.Action("keycheck") %>',
            data: { key : key },
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
            
            <%= Html.Hidden("Id") %>
            
            <label for="Name">Name</label>
            
            <%= Html.TextBox("Name", new { @class="required", size="40", maxLength="100"}) %>
            <span class="info">The name of the conference.</span>
            
            <label for="Key">Unique Key</label>
            <%= Html.TextBox("Key", new { @class = "uniqueKey required", size = "25", maxLength = "25" })%>
            <span class="info">A unique name to identify the conference.  This will be used in a url, so it must not contain illegal characters such as spaces or symbols.</span>
            
            <label for="StartDate">Starts</label>        
            <%= Html.TextBox("StartDate", new { @class = "required", size = "25", maxLength = "10" })%>
            
            <label for="EndDate">Ends</label>        
            <%= Html.TextBox("EndDate", new { @class = "required", size = "25", maxLength = "10" })%>
            
            <label for="Description">Description</label>
            <%= Html.TextArea("Description", new{ rows=7, cols=60, @class = "required" })%>
            <span class="info">Max 1000 characters.  No formatting.</span>
            
            <label for="PubliclyVisible">Visible to the public</label>
            <%= Html.CheckBox("PubliclyVisible") %>
            
            <div class="button-row">
                <input type="submit" id="conf_save" value="Save" class="submit" />
            </span>
        </fieldset>    
    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
