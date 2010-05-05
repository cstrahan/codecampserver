rem @echo off
if ~%1~ == ~~ (
    echo "pstrami method argument is required"
    powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command "& { import-module .\pstrami.psm1 ; get-module | where-object {$_.name -eq 'pstrami'} | select-object 'ExportedCommands'  }"
    goto end
)
@echo %*
powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command "& { import-module .\pstrami.psm1 ; %* ; if ($lastexitcode -ne 0) {write-host "ERROR: $lastexitcode" -fore RED; exit $lastexitcode} }"
:end