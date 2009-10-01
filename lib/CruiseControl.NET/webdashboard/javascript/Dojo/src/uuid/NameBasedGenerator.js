
dojo.provide("dojo.uuid.NameBasedGenerator");dojo.uuid.NameBasedGenerator=new function(){this.generate=function(returnType){dojo.unimplemented("dojo.uuid.NameBasedGenerator.generate");var returnValue="00000000-0000-0000-0000-000000000000";if(returnType&&(returnType!=String)){returnValue=new returnType(returnValue);}
return returnValue;};}();