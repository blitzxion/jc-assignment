@echo off
setlocal enabledelayedexpansion
cls
SET ngt=".\tools\nuget.exe"
for /f "usebackq tokens=*" %%i in (`.\tools\vswhere -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe`) do (
    SET msb="%%i" %*
    goto exit_for
)
:exit_for

echo Restoring packages...
%ngt% restore 
echo All packages restored.

echo Compiling solution...
%msb% JumpCloudAssignment.sln /m /t:Rebuild /p:Configuration=Release 
echo Compile complete.