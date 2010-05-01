

function Receive-Package( $applicationName,$databaseServer,$instance,$reloadData) {

    $codedir="..\codeToDeploy_$instance\"
    $appinstance="$applicationName_$instance"

    if(test-path $codedir){ remove-item $codedir -Recurse }

    & ".\$($applicationName)Package.exe" "-o$codedir" -y
    
    set-location $codedir

    & ".\CommonDeploy.bat" "$databaseServer" "$instance" "$reloadData"        
}


function Send-Package
{  
param([string]$server,[string]$destinationDirectory,[string]$cmd);
"args $cmd"
    $msdeployexe = resolve-path "..\lib\msdeploy\msdeploy.exe"
    
    $sourceDirPath = resolve-path .
    remove-item send-package.log
    .$msdeployexe "-verb:sync" "-source:dirPath=$sourceDirPath" "-dest:dirPath=$destinationDirectory,computername=$server"        
    
    cmd.exe /c "$msdeployexe -verb:sync -dest:auto,computername=$server -source:runCommand=`"$destinationDirectory\pstrami-agent.bat $destinationDirectory $cmd`",waitInterval=2500,waitAttempts=20" | out-file "send-package.log"
    
    if(-not (select-string -path send-package.log -pattern "BUILD SUCCEEDED"))
    {
    get-content send-package.log | write-host
        "Send-Package Failed"
        exit '-1'
    }
    "Send-Package Succedded"
}


Export-ModuleMember Receive-Package, Send-Package
