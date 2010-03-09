<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.Core.Domain.Bases" %>


  <h2>Administration</h2>  
  <ul>
        <li><a href="<%=CodeCampServer.UI.Helpers.Extensions.UrlHelperExtensions.Action<UserController>(Url, c=>c.Edit((User)null)) %>"> My Profile</a></li>
          <li><a href="<%=CodeCampServer.UI.Helpers.Extensions.UrlHelperExtensions.Action<UserController>(Url, c=>c.Index()) %>"> Edit Users</a></li>
  </ul>

