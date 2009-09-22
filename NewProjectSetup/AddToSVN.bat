cd ..
lib\addtosvn\addtosvn.exe lib\subversion\svn.exe .
echo bin>ignore.txt
echo obj>>ignore.txt
lib\subversion\svn.exe propset svn:ignore -Fignore.txt -R src
del ignore.txt
pause