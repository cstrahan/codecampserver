
function Export-Database {
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

function Create-WindowsSqlLogin($accountName,$instance,$admin) {
    Add-PSSnapin SqlServerCmdletSnapin100 
    $hostname = hostname
    $loginName = "$hostname\$accountName"
    invoke-sqlcmd -ServerInstance $instance -Query "CREATE LOGIN [$loginName] FROM WINDOWS"
    if($admin) {
        invoke-sqlcmd -ServerInstance $instance -Query "sp_addsrvrolemember '$loginName', 'sysadmin'"
    }
}

function Create-WindowsDatabaseLogin($accountName,$instance,$database){
    Add-PSSnapin SqlServerCmdletSnapin100 
    $hostname = hostname
    $loginName = "$hostname\$accountName"
    invoke-sqlcmd -ServerInstance $instance -Query "Use $database CREATE USER [$accountName] FOR LOGIN [$loginName]"    
    invoke-sqlcmd -ServerInstance $instance -Query "Use $database EXEC sp_addrolemember 'db_datareader', '$accountName'"
    invoke-sqlcmd -ServerInstance $instance -Query "Use $database EXEC sp_addrolemember 'db_datawriter', '$accountName'"
}

Export-ModuleMember Export-Database , Create-WindowsSqlLogin, Create-WindowsDatabaseLogin