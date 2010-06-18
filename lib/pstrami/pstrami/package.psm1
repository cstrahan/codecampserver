function Extract-Package
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

function Create-Package
{
    param([string] $path,[string]$filename)
    dir $path| out-zip $filename
}

function out-zip
{
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

Export-ModuleMember Extract-Package , Create-Package