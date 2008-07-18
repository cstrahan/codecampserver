<%@ Import Namespace="System.Collections.Generic"%>
<%@ Import Namespace="CodeCampServer.Model"%>
<%@ Import Namespace="MvcContrib"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>

<p>
	<asp:placeholder id="flash" runat="server" />
</p>

<script runat="server">
	protected override void OnLoad(EventArgs e)
	{
		var flashMessages = (IEnumerable<FlashMessage>) ViewData.Model;
		foreach (var message in flashMessages)
		{
			var panel = new Panel();
			switch (message.Type)
			{
				case FlashMessage.MessageType.Message:
					panel.CssClass = "flash message";
					break;
				case FlashMessage.MessageType.Error:
					panel.CssClass = "flash error";
					break;
			}

			panel.Controls.Add(new LiteralControl(message.Message));
			flash.Controls.Add(panel);
		}
		
		base.OnLoad(e);
	}
</script>