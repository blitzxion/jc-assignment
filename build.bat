@echo off
cls
SET ngt=".\tools\nuget.exe"
SET msb="C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\msbuild.exe"

echo Restoring packages...
%ngt% restore 
echo All packages restored.

echo Compiling solution...
%msb% JumpCloudAssignment.sln /m /t:Rebuild /p:Configuration=Release 
echo Compile complete.