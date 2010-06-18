Function Get-ScheduleService
{
  New-Object -ComObject schedule.service
} #end Get-ScheduleService
Function Get-Tasks($folder)
{ #returns a task object
 $folder.GetTasks(1)
} #end Get-Tasks

Function Get-Task($folder,$name)
{ #returns a task object
 $folder.GetTask($name)
} #end Get-Tasks

Function New-TaskObject($path)
{ 
#returns a taskfolder object
 $taskObject = Get-ScheduleService
 $taskObject.Connect()
 if(-not $path) { $path = "\" }
 $taskObject.GetFolder($path)
} #end New-TaskObject

#New-Task -command "BatchJobs.Console.exe" -path "\CodeCampServer\DEV" -name "FileWatcher" -daily "1" -workingDirectory $workingDir -arguments "filewatcher"
#New-Task -command "BatchJobs.Console.exe" -path "\CodeCampServer\DEV" -name "Import File" -repeat "PT1M" -workingDirectory $workingDir -arguments "importfile"
Function New-ScheduledTask{
param(  $path,
        $Name,
        $description,
        $author,
        $command,
        $user,
        $password,
        $sddl,
        $repeat,
        $workingDirectory,
        $arguments,
        $daily)


  New-Variable -Name TASK_TRIGGER_TIME -Value 1 -Option constant
  New-Variable -Name TASK_TRIGGER_IDLE -Value 6 -Option constant
  New-Variable -Name TASK_TRIGGER_DAILY -Value 2 -Option constant
  New-Variable -Name TASK_ACTION_EXEC -Value 0 -Option constant
  New-Variable -Name CREATE_OR_UPDATE -Value 6 -Option constant
  New-Variable -Name TASK_LOGON_NONE -Value 0 -Option constant
  $user=$password=$sddl=$null
  $taskObject = Get-ScheduleService
  $taskObject.Connect()
  if(-not $path) { $path = "\" }
  $rootFolder = $taskObject.GetFolder($path)
  $taskdefinition = $taskObject.NewTask($null)
  $regInfo = $taskdefinition.RegistrationInfo
  if(-not $description) { $description = "Created by script" }
  $regInfo.Description = $description
  if(-not $author) { $author = $env:username }
  $regInfo.Author = $author
  $settings = $taskdefinition.Settings
  $settings.StartWhenAvailable = $true
  $settings.Hidden = $false
  $triggers = $taskdefinition.Triggers
  
  
   if($daily){
         $trigger = $triggers.Create($TASK_TRIGGER_DAILY)
         $trigger.DaysInterval=$daily
     }
  else {
        $trigger = $triggers.Create($TASK_TRIGGER_TIME)
    }
    
  $trigger.StartBoundary = "2009-12-23T00:00:00"
  
  if($repeat){   $trigger.Repetition.Interval=$repeat  }
  
  $action = $taskdefinition.Actions.Create($TASK_ACTION_EXEC)
  $action.Path = $command
  if($arguments){$action.Arguments=$arguments}
  if($workingDirectory){  $action.WorkingDirectory=$workingDirectory}
  

  $rootFolder.RegisterTaskDefinition($Name,$taskdefinition,$CREATE_OR_UPDATE,
                                     $user,$password,$TASK_LOGON_NONE,$sddl)

return 
} #end New-Task

Function Get-ScheduleService
{
  New-Object -ComObject schedule.service
} #end Get-ScheduleService

Function New-TaskObject($path)
{ #returns a taskfolder object
 $taskObject = Get-ScheduleService
 $taskObject.Connect()
 if(-not $path) { $path = "\" }
 $taskObject.GetFolder($path)
} #end New-TaskObject

Function Get-TaskFolder($folder,[switch]$recurse)
{ #returns a string representing the path to a task folder
  if($recurse)
    {
     $colFolders = $folder.GetFolders(0)
      foreach($i in $colFolders)
        {
          $i.path
          $subFolder = (New-taskObject -path $i.path)
           Get-taskFolder -folder $subFolder -recurse
        }
    } #end if
  ELSE
    {
     $folder.GetFolders(0) |
      foreach-object { $_.path }
     } #end else
} #end Get-TaskFolder

Function New-TaskFolder($folder,$path)
{ 
 $folder.createFolder($path)
} #end New-TaskFolder

Function Remove-TaskFolder($folder,$path)
{
 $folder.DeleteFolder($path,$null)
} #end Remove-TaskFolder

Export-ModuleMember New-ScheduledTask