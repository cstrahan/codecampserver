$databaseServer  = $args[0]
$projectName     = $args[1]
$reload          = $args[2]

$loadDataTestAssembly="$projectName.IntegrationTests.dll"
$databaseName=$projectName
$websiteTargetDir="C:\inetpub\$projectName"
$connectionString="Data Source=$databaseServer;Initial Catalog=$databaseName;Integrated Security=SSPI;"
$databaseIntegrated=$true


$databasescriptdirectory ="Database" 

function deploy( $reloaddatabase ){

    if($reloaddatabase -eq $true) {
        createDatabase
    }
    else {
        updateDatabase
    }

    updateConfigurationFile "website/bin/hibernate.cfg.xml"    
    

    delete $websiteTargetDir 
    
    
    copy-item website -destination $websiteTargetDir -recurse

    if($reloaddatabase -eq $true) {
        loadDevelopmentData
    }
}
function createDatabase(){ 
    & "tools\DatabaseDeployer.exe" "Rebuild" $databaseServer $databaseName $databasescriptdirectory  
}
function updateDatabase(){
    & "tools\DatabaseDeployer.exe" "Update" $databaseServer $databaseName $databasescriptdirectory  
 }
function loadDevelopmentData(){ 
 updateConfigurationFile "tests/hibernate.cfg.xml"
 & "tests\tools\nunit\nunit-console.exe" /include=DataLoader tests\$loadDataTestAssembly
}

function updateConfigurationFile ($filename) {
        $webConfigPath = Resolve-Path $filename
        $config = New-Object XML 
        $config.Load($webConfigPath) 
        $ns = New-Object Xml.XmlNamespaceManager $config.NameTable
        $ns.AddNamespace( "e", "urn:nhibernate-configuration-2.2" )
        $config.SelectSingleNode("//e:property[@name = 'connection.connection_string'] ",$ns).innertext = $connectionString
        $config.Save($webConfigPath) 
} 

function delete($dir){
    if(test-path $dir ){ remove-item $dir -Recurse -Force}
}

deploy -reloadDatabase $true  
