If you are deploying Code Camp Server to IIS5 or IIS6, then you'll have to install this isapi filter on the site in order to get the extensionless routes.

Directions:
  * copy the contents of the isapi folder into a folder on the server.  
    I put it in c:\inetpub\isapi.
  * create a log directory.  I used c:\inetpub\logs.  
    DO NOT PUT THE LOGS INTO THE ISAPI FOLDER.
  * rename the appropriate ini file from the rewrite rules folder to IsapiRewrite4.ini
    
    -  if you're deploying to the site root (ie:  http://mycodecamp.com) 
       then you'll want to copy IsapiRewrite4.root.ini
       
    -  if you're deploying to a virtual directory under the root 
       (ie:  http://mysite.com/codecamp ) then you'll want to copy
       IsapiRewrite4.vdir.ini
       
    (I recommend copy & pasting so you can keep the original)
    
  *  edit the IsapiRewrite4.ini and check the log path.  If you put your log in a
     different directory then make sure this matches.
     
  *  Edit SampleUrls.txt to see a list of URL patterns that you might want to test.  
  
  *  Run runtests.bat to see the urls piped through the system & see the results.
  
  
  *  Open Internet Information Services manager.  Right click on the site and go to properties.  Click on the ISAPI Filters tab.  Add a new isapi filter, named "ISAPI Rewrite".  Browse to the isapi filter dll from step 1.