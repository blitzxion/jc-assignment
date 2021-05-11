@echo off
cls

SET ngt=".\tools\nuget.exe"
SET msb="C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\msbuild.exe"
SET test=".\packages\xunit.runner.console.2.4.1\tools\net472\xunit.console.exe"
SET testPath=".\JumpCloudAssignment.Tests\bin\Release\JumpCloudAssignment.Tests.dll"

echo Restoring packages...
%ngt% restore -noninteractive -verbosity quiet
echo All packages restored.

echo Compiling solution...
%msb% JumpCloudAssignment.sln /m /nologo /verbosity:quiet /t:Rebuild /p:Configuration=Release
echo Compile complete.

echo Starting tests...
%test% %testPath% -nologo -verbose
echo Tests complete.
