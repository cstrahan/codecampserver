
function getLastValue(sourceArray,attribute)
{if(sourceArray.length==0)
{return"";}
return sourceArray[sourceArray.length-1][attribute];}
function average(sourceArray,attribute,predicate)
{var filteredList=select(sourceArray,predicate);if(filteredList.length==0)
{return 0;}
var itemCount=filteredList.length;var totalValue=sum(filteredList,attribute,null);var avg=(totalValue/itemCount);return Math.round(avg*100)/100;}
function count(sourceArray,predicate)
{var filteredList=getFilteredList(sourceArray,predicate);return filteredList.length;}
function sum(sourceArray,attribute,predicate)
{var filteredList=getFilteredList(sourceArray,predicate);if(filteredList.length==0)
{return 0;}
var total=0;for(var i=0;i<filteredList.length;i++)
{var filteredListItem=filteredList[i];var itemValue=filteredListItem[attribute];if(isNaN(itemValue)||itemValue=='')
{itemValue=0;}
total+=parseFloat(itemValue);}
return total;}
function max(sourceArray,attribute,predicate)
{var filteredList=getFilteredList(sourceArray,predicate);if(filteredList.length==0)
{return 0;}
var maxValue=0;for(var i=0;i<filteredList.length;i++)
{maxValue=Math.max(maxValue,parseFloat(filteredList[i][attribute]));}
return maxValue;}
function min(sourceArray,attribute,predicate)
{var filteredList=getFilteredList(sourceArray,predicate);if(filteredList.length==0)
{return 0;}
var minValue=9999999999;for(var i=0;i<filteredList.length;i++)
{minValue=Math.min(minValue,parseFloat(filteredList[i][attribute]));}
return minValue;}
function distinct(sourceArray,attribute)
{var distinctList={};for(var i=0;i<sourceArray.length;i++)
{var value=sourceArray[i][attribute];if(distinctList[value]==null)
{distinctList[value]='';}}
var distinctArray=[];for(var attribute in distinctList)
{distinctArray.push(attribute);}
return distinctArray;}
function select(sourceArray,predicate)
{var resultList=[];for(var i=0;i<sourceArray.length;i++)
{var item=sourceArray[i];if(predicate==null||predicate(item))
{resultList.push(item);}}
return resultList;}
function getFilteredList(sourceArray,predicate)
{if(predicate!=null)
{return select(sourceArray,predicate);}
else
{return sourceArray;}}