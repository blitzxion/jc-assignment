Clear-Host

$ngt = ".\tools\nuget.exe"
$msb = "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\msbuild.exe"

Write-Host "Restoring packages..."
& $ngt restore 
Write-Host "All packages restored."

Write-Host "Compiling solution..."
& $msb JumpCloudAssignment.sln `
    /m `
    /t:Rebuild `
    /p:Configuration=Release
Write-host "Compile complete."