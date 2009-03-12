<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="System.Web.Mvc.ViewPage<UserGroupForm>"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" runat="server">
<% Html.RenderPartial("HomeMenu"); %>
</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
	 <div>
		    <h1><%=Model.Name%> <%Html.RenderPartial("EditUserGroupLink", Model); %></h1>		    
		    <div>  
	            <div>Key: <%=Model.Key%></div>
	            <div>Id: <%=Model.Id%></div>
	            <div>Name: <%=Model.Name%></div>
			</div>
			<%Html.RenderPartial("ConferenceList", ViewData.Get<ConferenceForm[]>()); %>
       </div>
</asp:Content>

