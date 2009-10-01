
dojo.provide("dojo.dnd.Sortable");dojo.require("dojo.dnd.*");dojo.dnd.Sortable=function(){};dojo.lang.extend(dojo.dnd.Sortable,{ondragstart:function(e){var dragObject=e.target;while(dragObject.parentNode&&dragObject.parentNode!=this){dragObject=dragObject.parentNode;}
return dragObject;}});