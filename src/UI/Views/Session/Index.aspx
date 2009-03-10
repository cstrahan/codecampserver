<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<SessionForm>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
	 <div>
		    <h1><%=Model.Title%> <%Html.RenderPartial("EditSessionLink", Model); %></h1>
		    
		    <div>  
						<p>Track: <%=Model.Track.Name %> Level: <%= Model.Level.DisplayName %> Room: <%=ViewData.Model.RoomNumber %></p>
						<p>Time: <%=Model.TimeSlot.StartTime%> to <%=ViewData.Model.TimeSlot.EndTime%></p>
						<p>Speaker: <a href="/<%=(Model.Conference.Key+"/speakers/"+Model.Speaker.Key).ToXHTMLLink()%>"><%=Model.Speaker.FirstName %> <%=ViewData.Model.Speaker.LastName %></a></p>
						<p>Session Abstract: <%=Model.Abstract %></p>
						<p><a href="<%= Model.MaterialsUrl %>">Session Materials</a></p>
			</div>
        </div>
</asp:Content>
