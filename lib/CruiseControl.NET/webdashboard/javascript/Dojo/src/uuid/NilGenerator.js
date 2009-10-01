
dojo.provide("dojo.uuid.NilGenerator");dojo.uuid.NilGenerator=new function(){this.generate=function(returnType){var returnValue="00000000-0000-0000-0000-000000000000";if(returnType&&(returnType!=String)){returnValue=new returnType(returnValue);}
return returnValue;};}();