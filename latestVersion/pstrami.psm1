<#
.SYNOPSIS
Send-Package will send pstrami and all the files in the current directory to the destination directory on a server.
It will than execute the installation package on the remote server.
.PARAMETER server
This is the remote server that the pastrami package will be push to.
#>
function Send-Package {  
    param(  [parameter(Mandatory=$true)] [string]$server,
            [parameter(Mandatory=$true)] [string]$destinationDirectory,
            [parameter(Mandatory=$true)] [string]$cmd,
            [string] $credentials="none",
            [string] $successMessage="BUILD SUCCEEDED"
             );    
    
    $cred = ""
    if($credentials -ne "none")
    {
        $cred = ",getCredentials=$credentials"
    }
        
    $msdeployexe= "C:\Program` Files\IIS\Microsoft` Web` Deploy\msdeploy.exe"
    
    $sourceDirPath = resolve-path .
    remove-item sync-package.log   -ErrorAction SilentlyContinue | out-null
    
    .$msdeployexe "-verb:sync" "-source:dirPath=$sourceDirPath" "-dest:dirPath=$destinationDirectory,computername=$server$cred"  | out-file sync-package.log 
    
    get-content sync-package.log | write-host
    
    "@echo off
    cd /D $destinationDirectory    
    powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command `"& { import-module .\pstrami.psm1 ;        Receive-Package $cmd ;if ($lastexitcode -ne 0) { write-host `"ERROR: $lastexitcode`" -fore RED }; stop-process `$pid }" | out-file bootstrap.bat -encoding ASCII

    .$msdeployexe "-verb:sync" "-dest:auto,computername=$server$cred" "-source:runCommand=bootstrap.bat,waitInterval=2500,waitAttempts=20" | out-file "send-package.log"
    
    get-content send-package.log | write-host
    
    if(-not (select-string -path send-package.log -pattern $successMessage))
    {    
        "Send-Package Failed"
        exit '-1'
    }
    
    "Send-Package Succeeded"
    remove-item sync-package.log
    remove-item send-package.log
    remove-item bootstrap.bat
}

function Receive-Package {

    param( 
    [parameter(Mandatory=$true)]
    [string]
    $applicationName, 
    [parameter(Mandatory=$true)]
    [string]
    $databaseServer,
    [parameter(Mandatory=$true)]
    [string]
    $instance,
    [parameter(Mandatory=$true)]
    [string]
    $reloadData) 

    
    $appinstance = "$($applicationName)_$($instance)" #the variable parsing needs to be surrounded with parens.
    
    $codedir="..\codeToDeploy_$appinstance\"

    if(test-path $codedir){ remove-item $codedir -Recurse }

    & ".\$($applicationName)Package.exe" "-o$codedir" -y
    
    set-location $codedir

    & ".\CommonDeploy.bat" "$databaseServer" "$appinstance" "$reloadData"        
}

    function Extract-Zip
    {
    	param([string]$zipfilename, [string] $destination)

    	if(test-path($zipfilename))
    	{	
    		$shellApplication = new-object -com shell.application
    		$zipPackage = $shellApplication.NameSpace($zipfilename)
    		$destinationFolder = $shellApplication.NameSpace($destination)
    		$destinationFolder.CopyHere($zipPackage.Items())
    	}
    }
    function out-zip{
        param([string]$path)
        $files = $input
          
        if (-not $path.EndsWith('.zip')) {$path += '.zip'} 

        if (-not (test-path $path)) { 
          set-content $path ("PK" + [char]5 + [char]6 + ("$([char]0)" * 18)) 
        } 

        $zip=resolve-path($path)
        $ZipFile = (new-object -com shell.application).NameSpace( "$zip" ) 
        $files | foreach {$ZipFile.CopyHere($_.fullname)} 
    }



function Export-Database{
    param([string]$dbname, [string]$server=".\SqlExpress",$output="$dbname.sql")
    
    function export() {
        [reflection.assembly]::LoadWithPartialName(”Microsoft.SqlServer.Smo”) | Out-Null
        $serverSmo = New-Object (’Microsoft.SqlServer.Management.Smo.Server’) -argumentlist $server
        
        $db = $serversmo.databases[$dbname]
       
        if($db){
            "exporting $dbname from $server"
            if (test-path $output) { remove-item $output }
            Dump-Db $db > $output
        }
    }

    function Dump-Db($db) {

        function Get-SmoObjects($db) {
            $all = $db.Tables 
            $all += $db.StoredProcedures
            $all += $db.Views
            $all += $db.UserDefinedFunctions
            $all | where {!($_.IsSystemObject)}
        }
        
        function Generate-ObjectsWithDependencies($objList, $parents) {
            Write-Host -noNewLine "  Calculating dependencies..."
            $depTree = $depWalker.DiscoverDependencies($objList, $parents)
            $orderedUrns = $depWalker.WalkDependencies($depTree)

            Write-Host "done"    
            Write-Host -noNewLine "  Writing script..."
        
            foreach($urn in $orderedUrns) {
                $smoObject = $serverSmo.GetSmoObject($urn.Urn)
                foreach($script in $scrp.EnumScript($smoObject)) {
                    $script
                    "GO"
                }
            }
            
            Write-Host "done"
        }

     

        $scrp = new-object ('Microsoft.SqlServer.Management.Smo.Scripter') ($db.Parent)
        $depWalker = New-Object ('Microsoft.SqlServer.Management.Smo.DependencyWalker') $db.Parent
        
        Write-Host -noNewLine "Stage 1/2: finding server objects..."
        $dbObjects = Get-SmoObjects $db
        Write-Host "done"
        
        Write-Host "Stage 2/2: Exporting Data"
        $scrp.Options.ScriptDrops = $False
        $scrp.Options.IncludeIfNotExists = $False
        
        $scrp.Options.ClusteredIndexes = $False
        $scrp.Options.DriAll = $False
        $scrp.Options.Indexes = $False
        $scrp.Options.Triggers = $False
        
        $scrp.Options.ScriptSchema = $False
        $scrp.Options.ScriptData = $True

        Generate-ObjectsWithDependencies $dbObjects $True
        Write-Host "Stage 2/2 complete" 
    }
    export
}


Export-ModuleMember Receive-Package, Send-Package , Extract-Zip , Export-Database
