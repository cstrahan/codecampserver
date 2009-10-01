
dojo.provide("dojo.uuid.RandomGenerator");dojo.uuid.RandomGenerator=new function(){this.generate=function(returnType){dojo.unimplemented("dojo.uuid.RandomGenerator.generate");var returnValue="00000000-0000-0000-0000-000000000000";if(returnType&&(returnType!=String)){returnValue=new returnType(returnValue);}
return returnValue;};}();