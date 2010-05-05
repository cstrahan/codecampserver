@echo off
if ~%1~ == ~~ (
    echo A pstrami method argument is required!
    echo please call one of the following methods.
    echo " 
    powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command "& { import-module .\pstrami.psm1 ; (Get-Module pstrami).ExportedCommands.GetEnumerator() | %%{ get-help $_.name } }"
    goto end
)
@echo %*
powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command "& { import-module .\pstrami.psm1 ; %* ; if ($lastexitcode -ne 0) {write-host "ERROR: $lastexitcode" -fore RED; exit $lastexitcode} }"
:end