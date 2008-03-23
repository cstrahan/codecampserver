<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/TwoColumn.Master" 
AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" 
Title="Code Camp Server - Schedule Administration" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">

   <style type="text/css">
        
        div#availabe-sessions
        {
        	width: 170px;        	
        }
        
        table#schedule
        {
        	
        }
        
        table#schedule tr th
        {
        	background-color: #eef;
        }
        
        table#schedule tr td.dropbox
        {
        	width: 160px;
        	height: 95px;
        	border: solid 1px #ccc;
        }
        
        div.session-box
        {
        	float: left;
        	width: 145px;
        	background-color: #ddd;
        	padding: 1px;
        	margin: 5px;
        	cursor: move;        	
        }
        
        div.session-box h5
        {
        	color: black;        	        	
        	margin: 0px;
        }
                
   </style>

</asp:Content>

<asp:Content ID="sideContent" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
    <div id="available-sessions">
    
        <div id="session-box-1" class="session-box">
            <h5>Doing XYZ in Visual Studio 2008</h5>
            <i>by Joe Blow</i>
        </div>
    
        <div id="session-box-2" class="session-box">
            <h5>Powershell FTW!</h5>
            <i>by Hank Houdini</i>
        </div>
        
        <div id="session-box-3" class="session-box">
            <h5>Scrummerfall</h5>
            <i>by Jimmy Johnson</i>
        </div>
        
        <div id="session-box-4" class="session-box">
            <h5>BizTalk ate my homework</h5>
            <i>by Peter Plump</i>
        </div>
        
        <div id="session-box-5" class="session-box">
            <h5>SubSonic for Mere Mortals</h5>
            <i>by Scott Smith</i>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    
    <table id="schedule">
        <tr>
            <th></th>
            <th id="track-5">Agile Track</th>
            <th id="track-6">2008 Track</th>
            <th id="track-7">Enterprise Track</th>
            <th id="track-8">Dev Tools Track</th>
            <th id="track-9">Other Track</th>
        </tr>
        
        <tr>
            <th>9-10</th>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
        </tr>
        <tr>
            <th>10-11</th>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
        </tr>

        <tr>
            <th>11-12</th>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
        </tr>

        <tr>
            <th>8-9</th>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
            <td class="dropbox"></td>
        </tr>

    </table>

</asp:Content>
