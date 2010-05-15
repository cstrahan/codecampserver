function Deploy-Package{
    param(
        [string]$EnvironmentName,
        [string]$Package,
        [boolean]$Install=$false)
        
    Load-Configuration
    
    $environment =  Get-Environments  | where-object {$_.Name -eq $EnvironmentName} 

    Assert ( $environment -ne $null ) "$EnvironmentName is not a valid EnvironmentName argument"
    
    $package = resolve-path $package
    
    foreach($server in $environment.Servers)  {
        "Deploying to " + $server.Name + " Roles: " + $server.Roles        
        $hostname = hostname
        if(($server.Name -eq $hostname) -OR ($server.Name -eq "localhost"))
        {
            Install-RemoteServer $server $Package $environment
            #Install-LocalServer $server.Name $Package $EnvironmentName
        }
        else
        {
            Install-RemoteServer $server $Package $environment
        }
    }    
}

function Install-LocalServer{
    param([string] $serverName,[string] $packagePath,[string]$environmentName,[boolean]$OneTime=$false)
    
    set-location $packagePath
    $roles = Get-Roles
	#$server.Name $enviornmentName
    Foreach($role in $roles)
    {        
        $global:env = $environmentName
        "Executing Role:$role"
		if($onetime)
        {
            invoke-command -scriptblock $role.FullInstall
        }
		
        invoke-command -scriptblock $role.Action
    }
	"Deployment Succeded"
}

function Get-ServerRoles
{
    param([string]$serverName,[string]$environmentName)
	
	$roles = New-Object System.Collections.ArrayList
	
	$env = Get-Environments | Where-Object {$_.Name -eq $EnvironmentName} 
    
	foreach($server in $env.Servers)
	{
		if($server.name -eq $serverName) {
			foreach($role in $server.Roles)	{
				Get-Roles | ? {
				if ($_.Name -eq $role){$roles.Add($_)}
				}
			}
		}
	}
    return $roles
}

function Install-RemoteServer{
    param(
        [object]  $server,
        [string]  $packagePath,
        [object]  $environment,
        [boolean] $OneTime=$false,
        [string] $successMessage = "Deployment Succeded")
    "Install-RemoteServer"
    
    copy-item "pstrami.psm1"  $packagePath
    
    Send-Files $packagePath $server.Name $environment.InstallPath $server.Credential

    Create-RemoteBootstrapper $server.Name $environment.InstallPath $environmentName | out-file bootstrap.bat -encoding ASCII

    $result = Invoke-RemoteCommand $server.Name bootstrap.bat $server.Credential
    
    remove-item bootstrap.bat

    write-host $result    
    if(-not (select-string -inputobject $result -pattern $successMessage))
    {    
        "Install-RemoteServer Failed"
        exit '-1'
    }    
    "Install-RemoteServer Succeeded"
}

function Invoke-RemoteCommand{
    param(  [string] $server,
            [string] $cmd,
            [string] $cred="")
    
    if($cred -ne "")
    {
        $cred = ",getCredentials=" + $cred
    }
    $msdeployexe= "C:\Program` Files\IIS\Microsoft` Web` Deploy\msdeploy.exe"    
    .$msdeployexe "-verb:sync" "-dest:auto,computername=$server$cred" "-source:runCommand=$cmd,waitInterval=2500,waitAttempts=20" | out-file "send-package.log"
    
    $result = get-content send-package.log 
    remove-item send-package.log   
    return $result    
}

function Send-Files{
    param(  [string] $packagePath,
            [string] $server,
            [string] $remotePackagePath,
            [string] $cred)
    "Sending Files to $server : $remotePackagePath"        
    $msdeployexe= "C:\Program` Files\IIS\Microsoft` Web` Deploy\msdeploy.exe"    
   
   if(-not( test-path $packagePath+"pstrami.psm1"  ))
    {
        copy-item *.* $packagePath
    }

    remove-item sync-package.log   -ErrorAction SilentlyContinue | out-null    
    .$msdeployexe "-verb:sync" "-source:dirPath=$packagePath" "-dest:dirPath=$remotePackagePath,computername=$server$cred"  "-skip:objectName=filePath,absolutePath=.*\.log" | out-file sync-package.log     
    get-content sync-package.log | write-host
}


function Create-RemoteBootstrapper{
    param(  [string]$server,
            [string] $remotePackagePath,
            [string] $EnvironmentName)
            
    return "@echo on
    cd /D $remotePackagePath    
    powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command `"& { import-module .\pstrami.psm1; Load-Configuration;Install-LocalServer $server $remotePackagePath $EnvironmentName;if ($lastexitcode -ne 0) { write-host `"ERROR: $lastexitcode`" -fore RED }; stop-process `$pid }"
}

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
    
    .$msdeployexe "-verb:sync" "-source:dirPath=$sourceDirPath" "-dest:dirPath=$destinationDirectory,computername=$server$cred"  "-skip:objectName=filePath,absolutePath=.*\.log" | out-file sync-package.log 
    
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


function Assert { [CmdletBinding(
    SupportsShouldProcess=$False,
    SupportsTransactions=$False, 
    ConfirmImpact="None",
    DefaultParameterSetName="")]
	
	param(
	  [Parameter(Position=0,Mandatory=1)]$conditionToCheck,
	  [Parameter(Position=1,Mandatory=1)]$failureMessage
	)
	if (!$conditionToCheck) { throw $failureMessage }
}
		
function Load-Configuration{        
			
            if ($script:context -eq $null)
			{
				$script:context = New-Object System.Collections.Stack
			}
			
			$script:context.push(@{
			"roles" = @{}; #contains the deployment steps for each role        
            "environments" = @{};            
			
			})
            . .\pstrami.config.ps1            
   
            $script:context.Peek().roles |  format-list
<#
            foreach($key in $script:context.Peek().environments.Keys) 
        	{
        		$task = $script:context.Peek().environments.$key
        		$task
                foreach($server in $task.Servers)
                {
                    "Server: $($server.Name)"
                    foreach($role in $server.roles)
                    {                    
                        "Role: $role"
                    }
                }
        	}
  #>  
}

function Get-Environments{
    return $script:context.Peek().environments.Values
}

function Role {
    param(
    [Parameter(Position=0,Mandatory=1)]
    [string]$name = $null, 
    [Parameter(Position=1,Mandatory=1)]
    [scriptblock]$incremental = $null, 
    [Parameter(Position=1,Mandatory=1)]
    [scriptblock]$fullinstall = $null
    )
	$newTask = @{
		Name = $name
		Action = $incremental
        FullInstall = $fullinstall
	}
	
	$taskKey = $name.ToLower()
	
	Assert (!$script:context.Peek().roles.ContainsKey($taskKey)) "Error: Role, $name, has already been defined."
	
	$script:context.Peek().roles.$taskKey = $newTask
}

function Get-Roles {
    return $script:context.Peek().roles.Values
}

function Server {
    param(
    [Parameter(Position=0,Mandatory=1)]
    [string]$name, 
    [Parameter(Position=1,Mandatory=1)]
    [string[]]
    $roles = $null,
    [string] $credential=""
    )
	$newTask = "" | select-object Name,Roles
    $newTask.Name = $name
	$newTask.Roles = $roles
	
	return $newTask
}
	
function Environment {
    param(
    [Parameter(Position=0,Mandatory=1)]
    [string]$name, 
    [Parameter(Position=1,Mandatory=1)]
    [object[]]$servers ,
    [string] $installPath
    )
	$newTask = "" | select-object Name,Servers,InstallPath
	$newTask.Name = $name
	$newTask.Servers = $servers
    $newTask.InstallPath = $installPath
	
	
	$taskKey = $name.ToLower()
	
	Assert (!$script:context.Peek().environments.ContainsKey($taskKey)) "Error: Role, $name, has already been defined."
	
	$script:context.Peek().environments.$taskKey = $newTask
	#return $newTask
}


Export-ModuleMember Receive-Package, Send-Package, Load-Configuration, Deploy-Package,Install-LocalServer,get-serverroles,get-roles