properties {
  $projectName = "CodeCampServer"
  $base_dir = resolve-path .
  $build_dir = "$base_dir\build"
  $source_dir = "$base_dir\src\"
  $test_dir = "$build_dir\test\"
  $result_dir = "$build_dir\results\"
}

task default -depends privateBuild
task privateBuild -depends Clean, Compile, Test
task integrationBuild -depends Clean,Compile,TestWithCoverage,Inspection

task Test {  
    copy_all_assemblies_for_test
    run_nunit "$projectName.UnitTests.dll"
    run_nunit "$projectName.IntegrationTests.dll"
}

task Compile -depends Clean { 
    msbuild /t:build $source_dir$projectName.sln 
}

task Clean { 
    delete_directory $build_dir
    create_directory $test_dir 
    create_directory $result_dir
    msbuild /t:clean $source_dir\$projectName.sln 
}

task TestWithCoverage {
    copy_all_assemblies_for_test
    run_nunit_with_coverage "$projectName.UnitTests.dll"
    run_nunit_with_coverage "$projectName.IntegrationTests.dll"
}

task Inspection {
    run_fxcop
    run_source_monitor
}

# -------------------------------------------------------------------------------------------------------------
# generalized functions 
# --------------------------------------------------------------------------------------------------------------
function global:run_fxcop
{
   & .\lib\FxCop\FxCopCmd.exe /out:$result_dir"FxCopy.xml"  /file:$test_dir$projectname".*.dll" /quiet /d:$test_dir /c /summary | out-file $result_dir"fxcop.log"
}
function global:run_source_monitor
{
$command =  $result_dir + "command.xml"

"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<sourcemonitor_commands>
    <write_log>true</write_log>
    <command>
        <project_file>build\results\sm_project.smp</project_file>
        <project_language>CSharp</project_language>
        <source_directory>src</source_directory>
        <include_subdirectories>true</include_subdirectories>
        <checkpoint_name>0</checkpoint_name>
        <export>
            <export_file>build\results\sm_summary.xml</export_file>
            <export_type>1</export_type>
        </export>
    </command>
    <command>
        <project_file>build\results\sm_project.smp</project_file>
        <checkpoint_name>0</checkpoint_name> 
        <export>
            <export_file>build\results\sm_details.xml</export_file>
            <export_type>2</export_type>
        </export>
    </command> 
</sourcemonitor_commands>" | out-file $command -encoding "ASCII"

	.\lib\sourcemonitor\sourcemonitor.exe /C $command | out-null
    Convert-WithXslt -originalXmlFilePath $result_dir"sm_details.xml"  -xslFilePath  "lib\sourcemonitor\SourceMonitorSummaryGeneration.xsl" -outputFilePath $result_dir"sm_top15.xml" 

}
function global:delete_directory($directory_name)
{
  rd $directory_name -recurse -force  -ErrorAction SilentlyContinue 
}

function global:create_directory($directory_name)
{
  mkdir $directory_name  -ErrorAction SilentlyContinue 
}

function global:run_nunit ($test_assembly)
{
    & lib\nunit\nunit-console.exe $test_dir$test_assembly /nologo /nodots /xml=$result_dir$test_assembly.xml
}

function global:run_nunit_with_coverage($test_assembly)
{
     .\lib\ncover\NCover.Console.exe .\lib\nunit\nunit-console.exe $test_dir$test_assembly /noshadow /nologo /nodots  /xml=$result_dir$test_assembly.xml  //x $result_dir"$test_assembly.Coverage.xml"  //ias $projectName".Core;"$projectName".UI;"$projectName".Infrastructure;"$projectName".DependencyInjection" //w $test_dir //h $result_dir

}

function global:Copy_and_flatten ($source,$filter,$dest)
{
  ls $source -filter $filter -r | cp -dest $dest
}

function global:copy_all_assemblies_for_test{
  Copy_and_flatten $source_dir *.dll $test_dir
  Copy_and_flatten $source_dir *.config $test_dir
  Copy_and_flatten $source_dir *.xml $test_dir
  Copy_and_flatten $source_dir *.pdb $test_dir
}

function global:Convert-WithXslt($originalXmlFilePath, $xslFilePath, $outputFilePath) 
{
   ## Simplistic error handling
   $xslFilePath = resolve-path $xslFilePath
   if( -not (test-path $xslFilePath) ) { throw "Can't find the XSL file" } 
   $originalXmlFilePath = resolve-path $originalXmlFilePath
   if( -not (test-path $originalXmlFilePath) ) { throw "Can't find the XML file" } 
   #$outputFilePath = resolve-path $outputFilePath -ErrorAction SilentlyContinue 
   if( -not (test-path (split-path $originalXmlFilePath)) ) { throw "Can't find the output folder" } 

   ## Get an XSL Transform object (try for the new .Net 3.5 version first)
   $EAP = $ErrorActionPreference
   $ErrorActionPreference = "SilentlyContinue"
   $script:xslt = new-object system.xml.xsl.xslcompiledtransform
   trap [System.Management.Automation.PSArgumentException] 
   {  # no 3.5, use the slower 2.0 one
      $ErrorActionPreference = $EAP
      $script:xslt = new-object system.xml.xsl.xsltransform
   }
   $ErrorActionPreference = $EAP
   
   ## load xslt file
   $xslt.load( $xslFilePath )
     
   ## transform 
   $xslt.Transform( $originalXmlFilePath, $outputFilePath )
}