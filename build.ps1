Clear-Host

$ngt = ".\tools\nuget.exe"
$msb = .\tools\vswhere -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe | select-object -first 1

Write-Host "Restoring packages..."
& $ngt restore 
Write-Host "All packages restored."

Write-Host "Compiling solution..."
& $msb JumpCloudAssignment.sln `
    /m `
    /t:Rebuild `
    /p:Configuration=Release
Write-host "Compile complete."