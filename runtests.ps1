Clear-Host

$msb = .\tools\vswhere -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe | select-object -first 1
$ngt = ".\tools\nuget.exe"
$test = ".\packages\xunit.runner.console.2.4.1\tools\net472\xunit.console.exe"
$testPath = ".\JumpCloudAssignment.Tests\bin\Release\JumpCloudAssignment.Tests.dll";

Write-Host "Restoring packages..."
& $ngt restore `
    -noninteractive `
    -verbosity quiet
Write-Host "All packages restored."

Write-Host "Compiling solution..."
& $msb JumpCloudAssignment.sln `
    /m `
    /nologo `
    /verbosity:quiet `
    /t:Rebuild `
    /p:Configuration=Release
Write-host "Compile complete."

Write-host "Starting tests..."
& $test $testPath `
    -nologo `
    -verbose
Write-Host "Tests complete."
