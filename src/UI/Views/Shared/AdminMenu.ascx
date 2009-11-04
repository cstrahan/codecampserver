<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>

  <div class="h100">
  <h1>Administration</h1>  
  <ul>
        <li><a href="<%=Url.Action<UserController>(c=>c.Edit((User)null)) %>"> My Profile</a></li>
          <li><a href="<%=Url.Action<UserController>(c=>c.Index()) %>"> Edit Users</a></li>
  </ul>
  </div>
