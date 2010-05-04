function Send-Package {  
    param([string]$server,[string] $credentials,[string]$destinationDirectory,[string]$cmd);    
    
    $cred = ""
    if($credentials -ne "none")
    {
        $cred = ",getCredentials=$credentials"
    }
    
    "@echo off
    cd /D $destinationDirectory    
    powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command `"& {         import-module .\pstrami.psm1 ;        Receive-Package $cmd ;        if ($lastexitcode -ne 0) {           write-host `"ERROR: $lastexitcode`" -fore RED         };        stop-process `$pid      }" | out-file bootstrap.bat -encoding ASCII
    
    $msdeployexe= "C:\Program` Files\IIS\Microsoft` Web` Deploy\msdeploy.exe"
    
    $sourceDirPath = resolve-path .
    remove-item send-package.log   -ErrorAction SilentlyContinue
    
    .$msdeployexe "-verb:sync" "-source:dirPath=$sourceDirPath" "-dest:dirPath=$destinationDirectory,computername=$server$cred" "-verbose" | out-file "sync-package.log"
    get-content sync-package.log | write-host
    
    .$msdeployexe "-verb:sync" "-dest:auto,computername=$server$cred" "-source:runCommand=bootstrap.bat,waitInterval=2500,waitAttempts=20" | out-file "send-package.log"
    
    
    if(-not (select-string -path send-package.log -pattern "BUILD SUCCEEDED"))
    {
        get-content send-package.log | write-host
        "Send-Package Failed"
        exit '-1'
    }
    "Send-Package Succeeded"
    remove-item send-package.log
    remove-item bootstrap.bat
}

function Receive-Package( $applicationName,$databaseServer,$instance,$reloadData) {

    
    $appinstance="$applicationName_$instance"
    $codedir="..\codeToDeploy_$instance\"

    if(test-path $codedir){ remove-item $codedir -Recurse }

    & ".\$($applicationName)Package.exe" "-o$codedir" -y
    
    set-location $codedir

    & ".\CommonDeploy.bat" "$databaseServer" "$appinstance" "$reloadData"        
}

Export-ModuleMember Receive-Package, Send-Package
