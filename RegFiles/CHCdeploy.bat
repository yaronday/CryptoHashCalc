@echo off
setlocal

echo === CryptoHashCalc Deployment Script ===

if "%CHC_PATH%"=="" (
    echo ERROR: CHC_PATH is not defined yet!
    echo Please set the CHC_PATH environment variable first,
	echo by running setCHCpath "your selected path"
	echo Example: setCHCpath "C:\MyCHCpath" 
@echo off	
    pause
    exit /b 1
)

rem Get the parent directory of the batch file's directory
for %%i in ("%~dp0..") do set "PARENT=%%~fi"

set "SRC=%PARENT%\bin\Release"

echo Source:      %SRC%
echo Destination: %CHC_PATH%
echo.

if not exist "%SRC%" (
    echo ERROR: Source directory does not exist.
    pause
    exit /b 1
)

if not exist "%CHC_PATH%" (
    echo Creating destination folder...
    mkdir "%CHC_PATH%"
)

echo Copying files...
copy /Y "%SRC%\*.exe" "%CHC_PATH%\" >nul
copy /Y "%SRC%\*.dll" "%CHC_PATH%\" >nul

echo.
echo Deployment complete.
echo Files copied to %CHC_PATH%
echo.

pause
endlocal
