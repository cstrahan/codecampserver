

function DeployLocal( $applicationName,$databaseServer,$instance,$reloadData ) {

    $codedir="..\codeToDeploy_$instance\"
    $appinstance="$applicationName_$instance"

    if(test-path $codedir){ remove-item $codedir -Recurse }

    & ".\$($applicationName)Package.exe" "-o$codedir" -y
    
    set-location $codedir

    & ".\CommonDeploy.bat" "$databaseServer" "$instance" "$reloadData"
}


function DeployRemote($server,$destinationDirectory,$cmdargs)
{  
    $msdeployexe = resolve-path "..\lib\msdeploy\msdeploy.exe"
    
    $sourceDirPath = resolve-path .

    .$msdeployexe "-verb:sync" "-source:dirPath=$sourceDirPath" "-dest:dirPath=$destinationDirectory,computername=$server"        
    
    cmd.exe /c "$msdeployexe -verb:sync -dest:auto,computername=$server -source:runCommand=`"$destinationDirectory\deploy1.bat $destinationDirectory $cmdargs`",waitInterval=2500,waitAttempts=15" 
    #$msdeployexe -verb:sync -dest:auto,computername=$server -source:runCommand=`"$destinationDirectory\deploy1.bat $destinationDirectory $cmdargs`""

    #$result = [regex]::matches($deployOutput,"Info: BUILD (?'setupResultCode'.+)").value


}

Export-ModuleMember DeployLocal, DeployRemote
