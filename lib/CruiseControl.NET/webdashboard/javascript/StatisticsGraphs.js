
var version="2.7";var _summarisedStatistics=[];var _recentStatistics=[];var _hasDetailedTableBeenRendered=false;var _hasSummaryTableBeenRendered=false;var _haveHistoricGraphsBeenRendered=false;var GraphTab={Recent:1,Historic:2};function convertTimeIntoSeconds(time)
{var timeParts=time.split(":");return timeParts[0]*3600+timeParts[1]*60+parseInt(timeParts[2]);}
function summariseStatistics()
{var lastDate="";var statsGroupedByDay={};if(_statistics.length==0)
{return statsGroupedByDay;}
var projectDays=distinct(_statistics,"Date");for(var dayIndex=0;dayIndex<projectDays.length;dayIndex++)
{var currentDate=projectDays[dayIndex];if(typeof(currentDate)=='string')
{currentDate=new Date(currentDate);}
statsGroupedByDay[currentDate.toDateString()]=[];}
var currentStatistic=[];for(var i=0;i<_statistics.length;i++)
{var statistic=_statistics[i];var statisticsDate=new Date(statistic.Date);var dayText=statisticsDate.toDateString();statsGroupedByDay[dayText].push(statistic);}
return generateDailySummaries(statsGroupedByDay);}
function getTimelineDays()
{var firstDate=new Date(_statistics[0].Date);var lastDate=new Date(_statistics[_statistics.length-1].Date);return generateDateRange(firstDate,lastDate);}
function prepareStatistics()
{var usedStats=getUsedStatisticAttributes();for(var i=0;i<_statistics.length;i++)
{var statistic=_statistics[i];statistic["index"]=i;statistic["DurationInSeconds"]=convertTimeIntoSeconds(statistic["Duration"]);statistic["TestsPassed"]=statistic["TestCount"]-statistic["TestFailures"]-statistic["TestIgnored"];for(var attributeIndex=0;attributeIndex<usedStats.length;attributeIndex++)
{var attributeName=usedStats[attributeIndex];statistic[attributeName]=zeroIfInvalid(statistic[attributeName]);}}}
function getUsedStatisticAttributes()
{var usedStats={};for(var configIndex=0;configIndex<_recentGraphConfigurations.length;configIndex++)
{var config=_recentGraphConfigurations[configIndex];for(var seriesIndex=0;seriesIndex<config.series.length;seriesIndex++)
{var series=config.series[seriesIndex];usedStats[series.attributeName]='';}}
var attributes=[];for(var attribute in usedStats)
{attributes.push(attribute);}
return attributes;}
function zeroIfInvalid(dataItem)
{if(dataItem==''||typeof(dataItem)=='undefined'||isNaN(dataItem))
{return'0';}
else
{return dataItem;}}
function getRecentStatistics(numberOfBuilds)
{var startIndex=Math.max(_statistics.length-numberOfBuilds,0);for(var i=startIndex;i<_statistics.length;i++)
{var clonedStatistic=cloneObject(_statistics[i]);clonedStatistic["index"]=_recentStatistics.length;clonedStatistic["label"]=clonedStatistic["BuildLabel"];_recentStatistics.push(clonedStatistic);}}
function cloneObject(sourceObject)
{var clone={};for(var attribute in sourceObject)
{clone[attribute]=sourceObject[attribute];}
return clone;}
function generateDateRange(startDate,endDate)
{var dayDifference=24*60*60*1000;var currentDate=startDate;var dateRange=[];endDate.setHours(23);endDate.setMinutes(59);while(currentDate<=endDate)
{dateRange.push(currentDate);currentDate=new Date(currentDate.getTime()+dayDifference);}
return dateRange;}
function generateDailySummaries(statsGroupedByDay)
{var lastBuildLabel="";var index=0;for(var day in statsGroupedByDay)
{var currentStatistics=statsGroupedByDay[day];var currentBuildLabel=getLastValue(currentStatistics,"BuildLabel");if(currentBuildLabel.length==0)
{currentBuildLabel=lastBuildLabel;}
var successfulBuilds=select(currentStatistics,successfulBuildsFilter);var failedBuilds=select(currentStatistics,failedBuildsFilter);var daySummary={day:day,index:index++,lastBuildLabel:currentBuildLabel};for(var attribute in _summaryConfiguration)
{daySummary[attribute]=_summaryConfiguration[attribute](successfulBuilds,failedBuilds);}
var dayDate=new Date(day);daySummary.label=daySummary.lastBuildLabel+"\n("+day+")";_summarisedStatistics.push(daySummary);lastBuildLabel=currentBuildLabel;}}
function successfulBuildsFilter(item)
{return(item["Status"]=="Success");}
function failedBuildsFilter(item)
{var status=item["Status"];return(status=="Failure"||status=="Exception");}
function processGraphList(configurationList,containerElement)
{for(var i=0;i<configurationList.length;i++)
{var graphOptions=configurationList[i];graphOptions.containerElement=containerElement;createGraph(graphOptions);}}
function createRecentGraphs()
{processGraphList(_recentGraphConfigurations,dojo.byId("RecentBuildsContainerArea"));}
function createHistoricGraphs()
{processGraphList(_historicGraphConfigurations,dojo.byId("HistoricGraphContainerArea"));}
function summaryDataTabChangeHandler()
{if(!_hasSummaryTableBeenRendered)
{ensureStatisticsHaveBeenSummarised();var tableContainerArea=dojo.byId("SummaryTableStatisticsContainerArea");generateStatisticsTable(tableContainerArea,"Build Summary Statistics",_summarisedStatistics,cellRenderer,true,summaryTableDrillDown);_hasSummaryTableBeenRendered=true;}}
function detailedDataTabChangeHandler()
{if(!_hasDetailedTableBeenRendered)
{var tableContainerArea=dojo.byId("DetailedTableStatisticsContainerArea");generateStatisticsTable(tableContainerArea,"Build Detailed Statistics",_statistics,cellRenderer,false,null);_hasDetailedTableBeenRendered=true;}}
function historicGraphsTabChangeHandler(evt)
{if(!_haveHistoricGraphsBeenRendered)
{ensureStatisticsHaveBeenSummarised();createHistoricGraphs();_haveHistoricGraphsBeenRendered=true;}}
function ensureStatisticsHaveBeenSummarised()
{if(_summarisedStatistics.length==0)
{summariseStatistics();}}
function setupLazyTabInitialization()
{var historicalTabWidget=dojo.widget.byId("HistoricalTabWidget");var detailedTabularTabWidget=dojo.widget.byId("DetailedDataTabWidget");var summarisedTabularTabWidget=dojo.widget.byId("SummarisedDataTabWidget");dojo.event.connect("before",historicalTabWidget,"show",historicGraphsTabChangeHandler);dojo.event.connect("before",detailedTabularTabWidget,"show",detailedDataTabChangeHandler);dojo.event.connect("before",summarisedTabularTabWidget,"show",summaryDataTabChangeHandler);}
dojo.addOnLoad(function()
{prepareStatistics();getRecentStatistics(20);setupLazyTabInitialization();window.setTimeout("createRecentGraphs()",100);});