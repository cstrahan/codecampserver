del %1\*.* /S /Q
rd %1
7za  x CodeCampServerPackage.zip -o%1 -r
