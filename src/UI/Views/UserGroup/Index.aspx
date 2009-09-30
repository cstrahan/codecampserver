<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="ViewPage<UserGroupInput>"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" runat="server">
<% Html.RenderPartial("HomeMenu"); %>
</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
	  
	 <div>
		    <h1><%=Model.Name%> <%Html.RenderPartial("EditUserGroupLink", Model); %></h1>		    
		    <div>  
	            <div>Name: <%=Model.Name%></div>
	            <div>Key: <%=Model.Key%></div>
	            <div>DomainName: <%=Model.DomainName%></div>
	            <div>Id: <%=Model.City%></div>
	            <div>Id: <%=Model.Region%></div>
	            <div>Name: <%=Model.Country%></div>
			</div>
			<div class="mt15">
			    <h4>Administrators</h4>
			    <ul>
			    <%foreach (var user in Model.Users){%>                                  
			    <li><%=user.Name%></li><%} %>
			    </ul>
			</div>
       </div>
</asp:Content>

