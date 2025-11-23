@echo off
rem ------------------------------------------------------------
rem Usage: setCHCpath.bat "C:\Your\Desired\Path"
rem ------------------------------------------------------------

if "%~1"=="" (
    echo ERROR: Please provide the install directory as a parameter.
    echo Example: %~nx0 "C:\YourDesiredPath"
    exit /b 1
)

set "InstallPath=%~1"

rem Remove wrapping quotes if accidentally double-quoted
set InstallPath=%InstallPath:"=%

setx CHC_PATH "%InstallPath%"

echo.
echo CHC_PATH permanently set to: %InstallPath%
echo (You may need to restart command prompt to use it.)
pause
