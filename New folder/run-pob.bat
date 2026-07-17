@echo off
echo Starting Path of Building 2...
:: Switch directory to PoB2 root so Lua scripts load correctly
cd /d "%~dp0\PathOfBuilding-PoE2"
:: Launch the exe with the literal filename "Path{space}of{space}Building-PoE2.exe"
start "" "%~dp0\PathOfBuilding-PoE2\runtime\Path{space}of{space}Building-PoE2.exe"
exit
