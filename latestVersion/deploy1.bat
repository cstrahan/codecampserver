cd %1
Shift
powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command "& { import-module .\deploy.psm1 ; DeployLocal %~1 %~2 %~3 %~4 ; if ($lastexitcode -ne 0) {write-host "ERROR: $lastexitcode" -fore RED; exit $lastexitcode}; stop-process $pid }" 
exit