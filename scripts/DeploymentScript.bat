@echo off
REM Grand Life Adventures - Auto-Deployment Script
REM This script automatically detects your GTA V installation and deploys the mod

echo.
echo ========================================
echo Grand Life Adventures - Deployment
echo ========================================
echo.

REM Check Rockstar Games Launcher / Default path
if exist "C:\Program Files\Rockstar Games\Grand Theft Auto V\scripts\" (
    echo Found GTA V at Rockstar Games Launcher location
    COPY "$(TargetPath)" "C:\Program Files\Rockstar Games\Grand Theft Auto V\scripts\"
    goto :success
)

REM Check Epic Games Store path
if exist "C:\Program Files\Epic Games\GTAV\scripts\" (
    echo Found GTA V at Epic Games Store location
    COPY "$(TargetPath)" "C:\Program Files\Epic Games\GTAV\scripts\"
    goto :success
)

REM Check Steam path
if exist "C:\Program Files (x86)\Steam\steamapps\common\Grand Theft Auto V\scripts\" (
    echo Found GTA V at Steam location
    COPY "$(TargetPath)" "C:\Program Files (x86)\Steam\steamapps\common\Grand Theft Auto V\scripts\"
    goto :success
)

echo ERROR: Could not find GTA V installation
echo Please ensure GTA V is installed and try again
goto :end

:success
echo.
echo ========================================
echo Installation successful!
echo Launch GTA V and press F9 to open the mod menu
echo ========================================
echo.
pause

:end
