powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command "& {Import-Module '.\psake.psm1'; invoke-psake .\default.ps1; if ($lastexitcode -ne 0) {write-host "ERROR: $lastexitcode" -fore RED; exit $lastexitcode} }" 



