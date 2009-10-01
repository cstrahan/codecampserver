
var Browser={isIE:!!(window.attachEvent&&!window.opera),isOpera:!!window.opera,isWebKit:navigator.userAgent.indexOf('AppleWebKit/')>-1,isGecko:navigator.userAgent.indexOf('Gecko')>-1&&navigator.userAgent.indexOf('KHTML')==-1};var AxisType={x:1,y:2};dojo.require("dojo.collections.Store");dojo.require("dojo.collections.Queue");dojo.require("dojo.charting.Chart");dojo.require('dojo.json');function Graph()
{var graphContainerArea=null;var graphContainer=null;var legendTable=null;var legendContainer=null;var heading=null;var store=new dojo.collections.Store();var dataSource=null;var series=[];var xTicks=[];var yAxisAttributeNames=[];this.numXTicks=5;this.numYTicks=5;this.yRange=null;this.xRange=null;this.dataType='integer';var determineRange=function(attributeName)
{var range={lower:min(dataSource,attributeName),upper:max(dataSource,attributeName)};return range;}
this.determineMultiSourceRange=function(attributeNames)
{var ranges=dojo.lang.map(attributeNames,determineRange);return{lower:min(ranges,"lower"),upper:max(ranges,"upper")};}
this.setDataSource=function(graphDataSource)
{store.setData(graphDataSource);dataSource=graphDataSource;}
this.setContainer=function(containerElement)
{if(graphContainerArea!=null&&graphContainerArea!=containerElement)
{document.removeElement(heading);document.removeElement(legendContainer);document.removeElement(graphContainer);}
graphContainerArea=containerElement;function createContainerElements()
{heading=document.createElement("h2");graphContainerArea.appendChild(heading);graphContainer=document.createElement("div");graphContainer.className="GraphContainer";graphContainer.innerHTML="Loading...";graphContainerArea.appendChild(graphContainer);legendContainer=document.createElement("div");legendContainer.className="Legend";graphContainerArea.appendChild(legendContainer);legendTable=document.createElement("table");legendContainer.appendChild(legendTable);}
createContainerElements();}
this.generatexAxisTickMarks=function(labelAttribute,numberTicks)
{numberTicks=Math.min(numberTicks,dataSource.length);var labels=[];var tickIndexDelta=parseInt(dataSource.length/numberTicks);var tickIndex=0;for(var i=0;i<=dataSource.length;i+=tickIndexDelta)
{var tickIndex=Math.min(dataSource.length-1,Math.round(i));var tickLabel=dataSource[tickIndex][labelAttribute].toString();labels.push({label:tickLabel,value:tickIndex});}
return labels;}
this.generateyAxisTickMarks=function(range,numberTicks)
{var labels=[];var rangeDelta=range.upper-range.lower;var tickDelta=rangeDelta/numberTicks;var tickLabel;var tickDataType=this.dataType;if(tickDelta<1)
{tickDataType='decimal';}
for(var tickValue=range.lower;tickValue<=range.upper;tickValue+=tickDelta)
{if(tickDataType=='integer')
{tickLabel=parseInt(tickValue);}
else
{tickLabel=Math.round(tickValue*100)/100;}
labels.push({label:tickLabel.toString(),value:tickValue});}
return labels;}
this.createAxis=function(axisValueAttributeNames,tickAttributeName,numberTicks,axisType)
{var axis=new dojo.charting.Axis();axis.showTicks=true;axis.showLines=true;axis.label="";if(axisType==AxisType.x)
{axis.origin="max";axis.range=this.xRange||this.determineMultiSourceRange(axisValueAttributeNames);}
else if(axisType==AxisType.y)
{axis.origin="min";axis.range=this.yRange||this.determineMultiSourceRange(axisValueAttributeNames);}
else
{alert('Invalid axis type specified');return axis;}
if(dataSource.length==0)
{return axis;}
var rangeDelta=axis.range.upper-axis.range.lower;if(rangeDelta==0)
{axis.range.lower=0;axis.labels.push({label:"0",value:0});axis.labels.push({label:axis.range.upper.toString(),value:axis.range.upper});return axis;}
if(axisType==AxisType.x)
{axis.labels=this.generatexAxisTickMarks(tickAttributeName,numberTicks);}
else if(axisType==AxisType.y)
{axis.labels=this.generateyAxisTickMarks(axis.range,numberTicks);}
if(Browser.isIE)
{for(var i=0;i<axis.labels.length;i++)
{axis.labels[i].label=axis.labels[i].label.replace("\n","<br/>");}}
return axis;}
this.setTitle=function(title)
{heading.innerHTML=title;}
this.addSeries=function(seriesName,seriesAttributeName,color)
{yAxisAttributeNames.push(seriesAttributeName);var seriesItem=new dojo.charting.Series({dataSource:store,bindings:{x:"index",y:seriesAttributeName},label:seriesName,color:color});series.push(seriesItem);var legendRow=legendTable.insertRow(legendTable.rows.length);var legendBoxCell=legendRow.insertCell(0);var legendTextCell=legendRow.insertCell(1);legendBoxCell.style.width="14px";var colorBoxDiv=document.createElement("div");colorBoxDiv.className="ColorBox";colorBoxDiv.style.backgroundColor=color;legendBoxCell.appendChild(colorBoxDiv);legendTextCell.innerHTML=seriesName;}
this.draw=function()
{var layoutOptions={};var xAxis=this.createAxis(["index"],"label",this.numXTicks,AxisType.x);var yAxis=this.createAxis(yAxisAttributeNames,"",this.numYTicks,AxisType.y);var chartPlot=new dojo.charting.Plot(xAxis,yAxis);dojo.lang.forEach(series,function(seriesItem)
{chartPlot.addSeries({data:seriesItem,plotter:dojo.charting.Plotters.CurvedArea});});var chartPlotArea=new dojo.charting.PlotArea();chartPlotArea.size={width:600,height:250};chartPlotArea.padding={top:20,right:20,bottom:50,left:50};chartPlotArea.plots.push(chartPlot);var chart=new dojo.charting.Chart(null,"Statistics Chart","");chart.addPlotArea({x:10,y:10,plotArea:chartPlotArea});chart.node=graphContainer;chart.render();}}
function getArrayOfAttributeValues(sourceArray,attributeName)
{var attributeValueArray=[];for(var i=0;i<sourceArray.length;i++)
{attributeValueArray.push(sourceArray[attributeName]);}
return attributeValueArray;}
function hasSeriesValues(dataSource,series)
{for(var index=0;index<dataSource.length;index++)
{var statistic=dataSource[index];for(var i=0;i<series.length;i++)
{var value=zeroIfInvalid(statistic[series[i].attributeName]);if(value!=0)
{return true;}}}
return false;}
function createGraph(options)
{if(options.dataSource.length<2||!hasSeriesValues(options.dataSource,options.series))
{return;}
var dataSource=options.dataSource;var graphContainer=options.containerElement;var graph=new Graph();graph.setDataSource(dataSource);graph.setContainer(graphContainer);graph.setTitle(options.graphName);graph.numXTicks=options.numXTicks||5;graph.numYTicks=options.numYTicks||5;graph.dataType=options.dataType||'integer';graph.xRange=options.xRange;graph.yRange=options.yRange;var series={};if(typeof(options.chartType)!='undefined')
{graph.chartType=options.chartType;}
for(var i=0;i<options.series.length;i++)
{var seriesItem=options.series[i];graph.addSeries(seriesItem.name,seriesItem.attributeName,seriesItem.color);}
_graphProcessingQueue.enqueue(graph,graph.draw);return graph;}
function ProcessingQueue()
{this.queue=new dojo.collections.Queue();this.isTimerActive=false;this.enqueue=function(targetObject,targetFunction)
{this.queue.enqueue({targetObject:targetObject,targetFunction:targetFunction});if(!this.isTimerActive)
{var processingQueue=this;window.setTimeout(function(){processingQueue.process()},150);this.isTimerActive=true;}}
this.process=function()
{if(this.queue.count>0)
{var queueItem=this.queue.dequeue();queueItem.targetFunction.apply(queueItem.targetObject);}
if(this.queue.count==0)
{this.isTimerActive=false;}
else
{var processingQueue=this;window.setTimeout(function(){processingQueue.process()},150);}}}
var _graphProcessingQueue=new ProcessingQueue();