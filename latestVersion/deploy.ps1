$application    = "CodeCampServer"
$dbserver       = $args[0]
$instance       = $args[1]
$reloadDatabase = $args[2]
$currentDir     = $args[3]

function SetDefaults( ){
    if( !$dbserver ){$dbserver=".\sqlexpress"}
    if( !$instance){$instance="local"}
    if( !$reloadDatabase ){$reloadDatabase=false}
}

SetDefaults

function DeployLocal( $applicationName,$databaseServer,$instance,$reloadData ) {

    $codedir="..\codeToDeploy_$instance\"
    $appinstance="$applicationName_$instance"

    if(test-path $codedir){ remove-item $codedir -Recurse }

    & ".\$($applicationName)Package.exe" "-o$codedir" -y
    
    set-location $codedir

    #cmd /c %systemroot%\system32\inetsrv\appcmd stop site $appinstance
    #iisreset
    & ".\CommonDeploy.bat" "$databaseServer" "$instance" "$reloadData"
    #cmd /c %systemroot%\system32\inetsrv\appcmd start site %appinstance%

    #DeployLocal -applicationName "$application" -databaseServer "$dbserver" -instance "$instance" -reloadData "$reloadDatabase"
}






function global::PushToServer($server,$destinationDirectory)
{
    $msdeployexe = ".\lib\msdeploy\msdeploy.exe"
    
    & $msdeployexe -verb:sync -source:dirPath=.\ -dest:dirPath="$destinationDirectory",computername=$server -verbose

    $deployOutput = & $msdeployexe -verb:sync -source:runCommand="$destinationDirectory\deploy1.bat $cmdargs $destinationDirectory",waitInterval=5000,waitAttempts=40 -dest:auto,computername=$server -verbose output="deploystep2.txt" 

    $result = [regex]::matches($deployOutput,"Info: BUILD (?'setupResultCode'.+)").value

    out-host $result
<#    
    <loadfile file="deploystep2.txt" property="deploystep2" />

    <regex pattern="Info: BUILD (?'setupResultCode'.+)" input="${deploystep2}" />
    <echo>Remote Build Result is ${setupResultCode}</echo>
    <if test="${string::contains(setupResultCode, 'FAILED')}">
      <fail message="Remote Nant Result was ${setupResultCode}" />
    </if>
  </target>
#>

}