<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
  <div class="h100">
  <h1>Administration</h1>  
  <ul>
        <li><a href="<%=Url.Action("editadminpassword","Admin") %>"> Update Admin Password</a></li>
        <li><a href="<%=Url.Action("index","Conference") %>"> Update Conference Information</a></li>
        <li>Update Conference Tracks</li>
        <li>Update Conference Timeslots</li>
        <li>Update Conference Sessions</li>
  </ul>
  </div>
