$script:projectName = "CodeCampServer";
$script:BaseTargetDir="C:\inetpub\$projectName" ;   
$script:ConnectionString="Data Source=$server;Initial Catalog=$($projectName)_$($environment);Integrated Security=SSPI;";


Environment "dev" -servers @( 
    Server "localhost" @( "Web"; "Db" ; "Application") ;) -installPath "c:\installs\codecampserver_dev"

Environment "ci" -servers @( 
    Server "173.203.67.149" @( "Web"; "Db" ; "Application") -credential 173_203_67_149;) -installPath "c:\installs\codecampserver_dev"

Environment "qa" -servers @( 
    Server "qa-web" @( "Web" ; "Application"); 
    Server "qa-db"  @("Db") -credential "qa_db"; 
    ) -installPath c:\installs\$projectName_qa

Role "Web" -Incremental {
    write-host "Installing the website"
    
    $destination = $BaseTargetDir + "_" + $env + "_website"
    
    Set-AppOffline $destination
    
    Delete-Dir $destination 
    
    Copy-Dir website $destination 
    
    Update-NHibernateConfiguration "$destination\bin\hibernate.cfg.xml" $ConnectionString  

    Update-WebConfigForProduction "$destination\web.config"
    
    Set-AppOnline $destination
        
} -FullInstall {
    #Create Appliction User
    #Create IIS Site
    #Set Credentials on App Pool
}

Role "Db" -Incremental {
    write-host "Installing the Database"
    $databaseName = $projectname + "_" + $env
    $databaseServer = ".\SqlExpress"

    # backup database
    
    & Database\Tools\DatabaseDeployer.exe "Update" $databaseServer $databaseName "database" 

} -FullInstall { 
    $databaseName = $projectname + "_" + $env
    $databaseServer = ".\SqlExpress"

    #create database
    #run migration
    & Database\Tools\DatabaseDeployer.exe "Create" $databaseServer $databaseName "database" 

    #load initial data
    Load-Data "$projectName.IntegrationTests.dll" $ConnectionString

    #create application login
    Create-WindowsDatabaseLogin "Application" $databaseServer $databaseName
}

Role "Application" -Incremental {
    write-host "Installing the Batch Application"
    $destination = $BaseTargetDir + "_" + $env + "_agents"
    
    # Disable Schedule Tasks
    # Stop Tasks and Services
    # Delete Existing Files
    # Copy New Files
    Copy-Dir Agents $destination 
    # Enable Tasks
    # Kick off tasks
} -FullInstall { 
    # create scheduled tasks
    # set security for scheduled tasks
    # Run Incremental 
}



#----------------------------------------------------------------------------------

function script:Update-NHibernateConfiguration ($filename,$connectionString) {
        "Updating $filename"
        $webConfigPath = Resolve-Path $filename
        $config = New-Object XML 
        $config.Load($webConfigPath) 
        $ns = New-Object Xml.XmlNamespaceManager $config.NameTable
        $ns.AddNamespace( "e", "urn:nhibernate-configuration-2.2" )
        $config.SelectSingleNode("//e:property[@name = 'connection.connection_string'] ",$ns).innertext = $connectionString
        $config.Save($webConfigPath) 
} 

function script:Delete-Dir($dir){
    if(test-path $dir ){ remove-item $dir -Recurse -Force}
}

function script:Copy-Dir{
    param($source,$destination)
    & xcopy $source $destination /S /I /Y /Q
}

function script:Load-Data{
    param($loadDataTestAssembly,$connectionString)
 Update-NHibernateConfiguration "tests/hibernate.cfg.xml" $connectionString
 & "tests\tools\nunit\nunit-console.exe" /include=DataLoader tests\$loadDataTestAssembly

}

function script:Update-WebConfigForProduction {
    param($filename)
    $xml = [xml](get-content $filename)
    $root = $xml.get_DocumentElement();
    $root."system.web".compilation.debug = "false"
    $root."system.web".customErrors.mode = "RemoteOnly"
    $xml.Save($filename)
}

function script:Set-AppOffline {
    param($destination)
    if(test-path $destination\app_offline.htm.bak){
        rename-item "$destination\app_offline.htm.bak"  "$destination\app_offline.htm" | out-null}
}

function script:Set-AppOnline 
{
    param($destination)
    if(test-path $destination\app_offline.htm){
        rename-item "$destination\app_offline.htm" "$destination\app_offline.htm.bak" | out-null}

}