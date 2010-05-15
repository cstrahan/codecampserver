function Create-WindowsLogin 
{
   param ([string]$accountName,$password ) 
   $hostname = hostname    
   $comp = [adsi] "WinNT://$hostname"
   $user = $comp.Create("User", $accountName)    
   $user.SetPassword($password)  
   $user.userflags = $user.userflags + 65536 # flag  
   $user.SetInfo()       
} 



function Add-UserToGroup
{
    param(  [string]$accountName,
            [string]$group)
   [string]$hostname=hostname
    $group = [ADSI]"WinNT://$hostname/$group,group"
    $group.add("WinNT://$hostname/$accountName")
} 
Export-ModuleMember Create-WindowsLogin , Add-UserToGroup