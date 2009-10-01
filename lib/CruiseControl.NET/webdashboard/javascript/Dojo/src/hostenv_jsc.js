
dojo.hostenv.name_='jsc';if((typeof ScriptEngineMajorVersion!='function')||(ScriptEngineMajorVersion()<7)){dojo.raise("attempt to use JScript .NET host environment with inappropriate ScriptEngine");}
import System;dojo.hostenv.getText=function(uri){if(!System.IO.File.Exists(uri)){return 0;}
var reader=new System.IO.StreamReader(uri);var contents:String=reader.ReadToEnd();return contents;}
dojo.hostenv.loadUri=function(uri){var contents=this.getText(uri);if(!contents){dojo.raise("got no back contents from uri '"+uri+"': "+contents);}
var value=dj_eval(contents);dojo.debug("jsc eval of contents returned: ",value);return 1;}
dojo.hostenv.println=function(s){print(s);}
dojo.hostenv.getLibraryScriptUri=function(){return System.Environment.GetCommandLineArgs()[0];}
dojo.requireIf((djConfig["isDebug"]||djConfig["debugAtAllCosts"]),"dojo.debug");