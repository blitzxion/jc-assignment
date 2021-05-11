Clear-Host

$ngt = ".\tools\nuget.exe"
$msb = "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\msbuild.exe"
$test = ".packages\xunit.runner.console.2.4.1\tools\net472\xunit.console.exe"

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
