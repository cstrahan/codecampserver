
var _urlRoot=document.location.href.replace(/(ViewStatisticsReport.aspx)/gi,"");function StringBuilder()
{this.buffer=[];this.append=function(string)
{this.buffer.push(string);};this.toString=function()
{return this.buffer.join("");};}
function createScrollableTable(tableId,sourceList,shouldWrapCellDelegate,cellRenderer,shouldDisplayDrillDownColumn,drillDownHandler)
{var html=new StringBuilder();html.append("<table id='"+tableId+"' cellpadding='0' cellspacing='0' border='0'><thead><tr class='TableHeader'>");if(shouldDisplayDrillDownColumn)
{html.append("<td style='width: 30px;'>&nbsp;</td>");}
var attributes=[];for(var attribute in sourceList[0])
{attributes.push(attribute);var headingText=splitAndCapatilizeWords(attribute);html.append("<td style='"+getColumnStyle(null,attributeName,shouldWrapCellDelegate)+"'>");html.append(headingText);html.append("</td>");}
html.append("</tr></thead><tbody>");for(var rowIndex=sourceList.length-1;rowIndex>=0;rowIndex--)
{var statistic=sourceList[rowIndex];var rowClass=getRowClass(statistic);if((rowIndex+1)%2==0)
{html.append("<tr class='AlternateRow "+rowClass+"'>");}
else
{html.append("<tr class='"+rowClass+"'>");}
if(shouldDisplayDrillDownColumn)
{html.append("<td class='DrillDown'><a title='Drill down' href='javascript:void(0)'>+</a></td>");}
for(var columnIndex=0;columnIndex<attributes.length;columnIndex++)
{var attributeName=attributes[columnIndex];var cellValue=cellRenderer(statistic,attributeName);html.append("<td style='"+getColumnStyle(statistic,attributeName,shouldWrapCellDelegate)+"'>");html.append(cellValue);html.append("</td>");}
html.append("</tr>");}
html.append("</tbody></table>");var fragment=document.createDocumentFragment();var scrollContainer=document.createElement("div");scrollContainer.className="ScrollContainer";scrollContainer.innerHTML=html.toString();fragment.appendChild(scrollContainer);var table=scrollContainer.childNodes[0];if(shouldDisplayDrillDownColumn)
{var numberRows=table.rows.length;for(var rowIndex=1;rowIndex<numberRows;rowIndex++)
{var row=table.rows[rowIndex];row.cells[0].childNodes[0].onclick=encloseDrillDownHandler(drillDownHandler,sourceList[numberRows-rowIndex-1],row);}}
return fragment;}
function encloseDrillDownHandler(handler,rowData,row)
{return function(){handler(rowData,row)};}
function getColumnStyle(currentRowData,attributeName,shouldWrapCellDelegate)
{var shouldWrapColumn=shouldWrapCellDelegate(currentRowData,attributeName);var cellStyle="white-space: nowrap";if(shouldWrapColumn)
{cellStyle="white-space: normal;";}
return cellStyle;}
function splitAndCapatilizeWords(name)
{var splitWord=name.replace(/([A-Z])/g," $1");var capatalizedWord=splitWord.charAt(0).toUpperCase()+splitWord.substring(1);return capatalizedWord;}
function generateStatisticsTable(tableContainerArea,reportName,sourceStatistics,cellRenderer,shouldDisplayDrillDown,drillDownHandler)
{var statisticsContainer=document.createElement("div");statisticsContainer.className="StatisticsTable";tableContainerArea.appendChild(statisticsContainer);var summaryHeading=document.createElement("h2");summaryHeading.innerHTML=reportName;statisticsContainer.appendChild(summaryHeading);var summaryTableContainer=createScrollableTable(reportName,sourceStatistics,shouldColumnWrap,cellRenderer,shouldDisplayDrillDown,drillDownHandler);statisticsContainer.appendChild(summaryTableContainer);}
function getRowClass(currentRowData)
{var status=currentRowData["Status"];return status||"";}
function summaryTableDrillDown(rowData,row)
{var hasDetails=(typeof(row.hasDetails)!='undefined');if(!hasDetails)
{setupDetailSubTable(rowData,row);}
else
{var table=row.parentNode.parentNode;var detailRow=table.rows[row.rowIndex+1];if(detailRow.className=='')
{detailRow.className='HideDetails';row.cells[0].childNodes[0].innerHTML="+";}
else
{detailRow.className='';row.cells[0].childNodes[0].innerHTML="-";}}}
function setupDetailSubTable(rowData,row)
{var table=row.parentNode.parentNode;var detailRow=table.insertRow(row.rowIndex+1);var indentCell=detailRow.insertCell(0);indentCell.innerHTML="&nbsp;";var detailCell=detailRow.insertCell(1);detailCell.colSpan=row.cells.length;detailRow.className='';row.hasDetails=true;var day=new Date(rowData["day"]);var detailStatistics=select(_statistics,function(item)
{var itemDate=new Date(item["Date"]);return(itemDate.valueOf()==day.valueOf());});var scrollTable=createScrollableTable("",detailStatistics,shouldColumnWrap,cellRenderer,false,null);scrollTable.childNodes[0].className="DetailSubTable";detailCell.appendChild(scrollTable);row.cells[0].childNodes[0].innerHTML="-";}
function cellRenderer(currentRowData,attributeName)
{var cellValue=currentRowData[attributeName];if(typeof(cellValue)=='string'&&cellValue.length==0)
{cellValue="&nbsp;";}
var cellHtml;switch(attributeName)
{case"Status":var textColorStyle='';if(cellValue=='Success')
{textColorStyle='color: green';}
cellHtml="<span style='"+textColorStyle+"'>"+cellValue+"</span>";break;case"BuildLabel":var wasSuccessful=(currentRowData["Status"]=='Success');var buildLabel=currentRowData["BuildLabel"];var startTimeText=currentRowData["StartTime"];var buildDateText=currentRowData["Date"];var timeRegEx=/\d{1,2}:\d{1,2}:\d{1,2}\s*((AM)|(PM)|())/gi;var timeMatch=startTimeText.match(timeRegEx);var buildDate=new Date(buildDateText+" "+timeMatch);var buildTime=buildDate.getFullYear().toString()+
zeroPadValue(buildDate.getMonth()+1,2)+
zeroPadValue(buildDate.getDate(),2)+
zeroPadValue(buildDate.getHours(),2)+
zeroPadValue(buildDate.getMinutes(),2)+
zeroPadValue(buildDate.getSeconds(),2);var url;if(wasSuccessful)
{url=_urlRoot+"build/log"+buildTime+"Lbuild."+buildLabel+".xml/ViewBuildReport.aspx";}
else
{url=_urlRoot+"build/log"+buildTime+".xml/ViewBuildReport.aspx";}
url=url.replace(/'/g,"%27");cellHtml="<a href='"+url+"'>"+cellValue+"</a>";break;default:cellHtml=cellValue;break;}
return cellHtml;}
function zeroPadValue(value,numDigits)
{value=value.toString();var numDigitsRequired=numDigits-value.length;for(var i=0;i<numDigitsRequired;i++)
{value="0"+value;}
return value;}
function shouldColumnWrap(currentRowData,attributeName)
{if(attributeName=='BuildErrorMessage')
{return true;}
var attribute;if(currentRowData!=null)
{attribute=currentRowData[attributeName];}
if(attribute==null)
{return false;}
return(attribute.toString().length>35);}