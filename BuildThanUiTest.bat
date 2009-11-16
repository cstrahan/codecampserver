call build.bat cruise %NANT_DATABASE_SERVER_PROPERTY_ARG_SQL2008X64%
cd build\package
run-ui-tests.bat
pause

