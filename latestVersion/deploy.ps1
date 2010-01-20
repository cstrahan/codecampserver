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

function DeployApp( $applicationName,$databaseServer,$instance,$reloadData ) {

    $codedir="..\codeToDeploy_$instance\"
    $appinstance="$applicationName_$instance"

    if(test-path $codedir){ remove-item $codedir -Recurse }

    & ".\$($applicationName)Package.exe" "-o$codedir" -y
    
    set-location $codedir

    #cmd /c %systemroot%\system32\inetsrv\appcmd stop site $appinstance
    #iisreset
    & ".\CommonDeploy.bat" "$databaseServer" "$instance" "$reloadData"
    #cmd /c %systemroot%\system32\inetsrv\appcmd start site %appinstance%
}

SetDefaults

DeployApp -applicationName "$application" -databaseServer "$dbserver" -instance "$instance" -reloadData "$reloadDatabase"
