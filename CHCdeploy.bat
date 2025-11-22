@echo off
setlocal

echo === CryptoHashCalc Deployment Script ===

REM set "SRC=C:\GitHubWS\WinFormApps\CryptoHashCalc\bin\Release"

set "SRC=%~dp0bin\Release"


set "DEST=C:\CryptoHashCalc"

echo Source:      %SRC%
echo Destination: %DEST%
echo.


if not exist "%SRC%" (
    echo ERROR: Source directory does not exist.
    pause
    exit /b 1
)

if not exist "%DEST%" (
    echo Creating destination folder...
    mkdir "%DEST%"
)

echo Copying .exe files...
copy /Y "%SRC%\*.exe" "%DEST%\" >nul

echo Copying .dll files...
copy /Y "%SRC%\*.dll" "%DEST%\" >nul

echo.
echo Deployment complete.
echo Files copied to %DEST%
echo.

pause
endlocal
