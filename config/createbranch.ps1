function CreateBranch($branchname) {
    $branchname=$branchname.Replace(" ","_")
    "Adding branch $branchname to ccnet.config"
    $template  = Get-Content branch.template.config
    $config = Get-Content ccnet.config
    
    
    $a = New-Object System.Collections.ArrayList
    foreach($line in $config)
    {
        if($line -notcontains "</cruisecontrol>" ){
            $a.Add($line)
        }
    }
    
    
    foreach ($arg in $template){
      $a.Add($arg.Replace("#BRANCH_NAME#",$branchname))
      }    
        
    $a.Add("</cruisecontrol>")  
    $a | out-file newCruise.config
}

CreateBranch $args[0]
