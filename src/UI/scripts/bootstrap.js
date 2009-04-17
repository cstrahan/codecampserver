function IncludeJavaScript(jsFile)
{
  document.write('<script type="text/javascript" src="'
    + jsFile + '"></scr' + 'ipt>');     
}
		
IncludeJavaScript('/scripts/jquery-1.2.6.min.js');
IncludeJavaScript('/scripts/date.js');
IncludeJavaScript('/scripts/jquery.datePicker.js');
IncludeJavaScript('/scripts/jquery.dataTables.js');
IncludeJavaScript('/scripts/documentready.js');
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));




